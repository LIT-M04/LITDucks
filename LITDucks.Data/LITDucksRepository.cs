using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LITDucks.Data
{
    public class LITDucksRepository
    {
        private string _connectionString;

        public LITDucksRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddRealDuckQuack()
        {
            using (var context = new LITDucksDataContext(_connectionString))
            {
                var record = context.QuackCounts.FirstOrDefault();
                if (record == null)
                {
                    record = new QuackCount { RealDuckCount = 1, RubberDuckCount = 0 };
                    context.QuackCounts.InsertOnSubmit(record);
                }
                else
                {
                    record.RealDuckCount++;
                }

                context.SubmitChanges();

            }
        }
        public void AddRubberDuckQuack()
        {
            using (var context = new LITDucksDataContext(_connectionString))
            {
                var record = context.QuackCounts.FirstOrDefault();
                if (record == null)
                {
                    record = new QuackCount { RealDuckCount = 0, RubberDuckCount = 1 };
                    context.QuackCounts.InsertOnSubmit(record);
                }
                else
                {
                    record.RubberDuckCount++;
                }

                context.SubmitChanges();
            }
        }

        public QuackCount GetCounts()
        {
            using (var context = new LITDucksDataContext(_connectionString))
            {
                return context.QuackCounts.FirstOrDefault();
            }
        }
    }
}
