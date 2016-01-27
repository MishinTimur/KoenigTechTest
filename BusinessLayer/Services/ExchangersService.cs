using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DTO;
using BusinessLayer.Exchange;
using DataAccessLayer.EF;

namespace BusinessLayer.Services
{
    public class ExchangersService : IDisposable
    {
        private readonly ApplicationDbContext mDbContext = ApplicationDbContext.Create();

        public ExchangersService()
        {
        }

        public async Task<ICollection<PaymentSystemInfo>> GetPaymentSystemsAsync()
        {
            var payments = await mDbContext.PaymentSystems.Include(a => a.Currency).ToListAsync();
            return payments.Select(a => (PaymentSystemInfo)a).ToList();
        }


        public async Task<ICollection<ExchangerInfo>> GetExchangersByPaymentsAsync(Guid giveSystemId, Guid getSystemId, double amount = 1, bool? calculateGive = null)
        {
            var exchangers = await mDbContext.Exchangers.
                Include(a => a.LocalIndexes.Select(b => b.PaymentSys.Currency)).
                Where(a => new[] { giveSystemId, getSystemId }.All(sysId => a.LocalIndexes.Any(b => b.PaymentSys.Id == sysId))).ToListAsync();
            var result = new List<ExchangerInfo>();
            try
            {
                foreach (var exchanger in exchangers)
                {
                    var giveLocalIndex = exchanger.LocalIndexes.First(a => a.PaymentSys.Id == giveSystemId);
                    var getLocalIndex = exchanger.LocalIndexes.First(a => a.PaymentSys.Id == getSystemId);

                    double giveIndexVal = giveLocalIndex.PaymentSys.Currency.GlobalIndex * giveLocalIndex.GetIndex;
                    double getIndexVal = getLocalIndex.PaymentSys.Currency.GlobalIndex * getLocalIndex.GiveIndex;

                    double giveCost = 0d, getCost = 0d;

                    CalculateCosts(amount, calculateGive, giveIndexVal, getIndexVal, out giveCost, out getCost);

                    result.Add(new ExchangerInfo()
                    {
                        Id = exchanger.Id,
                        Name = exchanger.Name,
                        Get = new PaymentInfo()
                        {
                            Cost = getCost,
                            PaymentSystem = (PaymentSystemInfo)getLocalIndex.PaymentSys
                        },
                        Give = new PaymentInfo()
                        {
                            Cost = giveCost,
                            PaymentSystem = (PaymentSystemInfo)giveLocalIndex.PaymentSys
                        }
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return result;
        }

        private static void CalculateCosts(double amount, bool? calculateGive, double giveIndexVal, double getIndexVal,
            out double giveCost, out double getCost)
        {
            // Автоматически определяем валюту, по которой идет расчет

            if (calculateGive == null)
            {
                if (giveIndexVal > getIndexVal)
                {
                    giveCost = amount*giveIndexVal/getIndexVal;
                    getCost = amount;
                }
                else
                {
                    giveCost = amount;
                    getCost = amount*getIndexVal/giveIndexVal;
                }
            }
            else
            {
                if (calculateGive.Value)
                {
                    giveCost = amount;
                    getCost = amount*getIndexVal/giveIndexVal;
                }
                else
                {
                    giveCost = amount*giveIndexVal/getIndexVal;
                    getCost = amount;
                }
            }
        }

        public void Dispose()
        {
            mDbContext.Dispose();
        }
    }
}
