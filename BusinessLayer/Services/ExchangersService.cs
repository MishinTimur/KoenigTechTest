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
    public class ExchangersManager
    {
        private readonly ApplicationDbContext mDbContext = ApplicationDbContext.Create();

        public ExchangersManager()
        {
        }

        public async Task<IEnumerable<PaymentSystemInfo>> GetPaymentSystemsAsync()
        {
            var payments = await mDbContext.PaymentSystems.ToListAsync();
            return payments.Select(a => (PaymentSystemInfo)a);
        }


        public async Task<IEnumerable<ExchangerInfo>> GetExchangersByPaymentsAsync(Guid giveSystemId, Guid getSystemId, double amount = 1, bool? calculateGive = null)
        {
            var exchangers = await mDbContext.Exchangers.
                Include(a => a.LocalIndexes.Select(b => b.PaymentSys.Currency)).
                Where(a => a.LocalIndexes.Any(b => b.PaymentSys.Id == giveSystemId || b.PaymentSys.Id == getSystemId)).ToListAsync();
            var result = new List<ExchangerInfo>();
            foreach (var exchanger in exchangers)
            {
                var giveLocalIndex = exchanger.LocalIndexes.First(a => a.PaymentSys.Id == giveSystemId);
                var getLocalIndex = exchanger.LocalIndexes.First(a => a.PaymentSys.Id == getSystemId);

                double giveGlobalIndexVal = giveLocalIndex.PaymentSys.Currency.GlobalIndex;
                double getGlobalIndexVal = getLocalIndex.PaymentSys.Currency.GlobalIndex;

                double giveCost = 0d, getCost = 0d;

                // Автоматически определяем валюту, по которой идет расчет
                if (calculateGive == null) 
                {
                    if (giveGlobalIndexVal > getGlobalIndexVal)
                    {
                        giveCost = (giveGlobalIndexVal/getGlobalIndexVal)*giveLocalIndex.GiveIndex*amount;
                        getCost = amount;
                    }
                    else
                    {
                        giveCost = amount;
                        getCost = (getGlobalIndexVal/giveGlobalIndexVal)*giveLocalIndex.GetIndex*amount;
                    }
                }
                else 
                {
                    if (calculateGive.Value)
                    {
                        giveCost = amount;
                        getCost = (getGlobalIndexVal / giveGlobalIndexVal) * giveLocalIndex.GetIndex * amount;
                    }
                    else
                    {
                        giveCost = (giveGlobalIndexVal / getGlobalIndexVal) * giveLocalIndex.GiveIndex * amount;
                        getCost = amount;
                    }
                }

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
            return result;
        }
    }
}
