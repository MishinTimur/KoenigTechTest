using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.EF
{
    public class AppDbContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var currencies = GenerateCurrencies(context);
            var paymentSystems = GeneratePaymentSystems(currencies, context);
            GenerateExchangers(paymentSystems, context);

            base.Seed(context);
        }

        // Курс на 27.01.2016 :)
        private IList<Currency> GenerateCurrencies(ApplicationDbContext context)
        {
            var res = new List<Currency>
            {
                new Currency() {Id = Guid.NewGuid(), Name = "USD", GlobalIndex = 1d},
                new Currency() {Id = Guid.NewGuid(), Name = "EUR", GlobalIndex = 0.9219d},
                new Currency() {Id = Guid.NewGuid(), Name = "RUB", GlobalIndex = 78.6287d},
                new Currency() {Id = Guid.NewGuid(), Name = "PLN", GlobalIndex = 4.1086d}
            };


            context.Currencies.AddRange(res);

            return res;
        }

        private IList<PaymentSystem> GeneratePaymentSystems(IList<Currency> currencies, ApplicationDbContext context)
        {
            var res = new List<PaymentSystem>();

            int totalCount = 0;
            var rand = new Random(DateTime.Now.Millisecond);
            foreach (var currency in currencies)
            {
                var systemsCount = rand.Next(4, 6);
                for (int i = 0; i < systemsCount; i++)
                {
                    res.Add(new PaymentSystem()
                    {
                        Id = Guid.NewGuid(),
                        Name = string.Format("SomePaymentSys{0}", ++totalCount),
                        Currency = currency
                    });
                }
            }

            context.PaymentSystems.AddRange(res);

            return res;
        }

        private void GenerateExchangers(IList<PaymentSystem> paymentSystems, ApplicationDbContext context)
        {

            var rand = new Random(DateTime.Now.Millisecond);
            int count = rand.Next(10, 15);
            for (int i = 0; i < count; i++)
            {
                var exchanger = new Exchanger()
                {
                    Id = Guid.NewGuid(),
                    Name = string.Format("SomeExchanger{0}", i + 1),
                    LocalIndexes = new List<LocalIndex>()
                };
                FillExchangerByLocalIndexes(exchanger, paymentSystems);
                context.Exchangers.Add(exchanger);
            }
        }

        private void FillExchangerByLocalIndexes(Exchanger exchanger, IList<PaymentSystem> paymentSystems)
        {
            var rand = new Random(DateTime.Now.Millisecond);
            double minBoundary = 0.95d;
            double maxBoundary = 1.05d;
            foreach (var paymentSystem in paymentSystems)
            {
                if (rand.Next()%2 == 0) continue;
                var localIndex = new LocalIndex()
                {
                    Id = Guid.NewGuid(),
                    GiveIndex = minBoundary + (rand.NextDouble()*(1d - minBoundary)),
                    GetIndex = 1d + (rand.NextDouble()*(maxBoundary - 1d)),
                    PaymentSys = paymentSystem,
                    Exchanger = exchanger
                };

                exchanger.LocalIndexes.Add(localIndex);
            }
        }
    }
}
