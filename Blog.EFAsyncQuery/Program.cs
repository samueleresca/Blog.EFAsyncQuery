using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Blog.EFAsyncQuery
{
    class Program
    {
        /// <summary>
        /// Main method 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            var task = QueryingAsync();

            Console.WriteLine("");
            Console.WriteLine("Main():   QueryingAsync() method has started");
            
           
            Console.WriteLine("Main():   QueryingAsync() task is waiting");
            Console.WriteLine("");
            
            task.Wait();
            Console.WriteLine("");
            Console.WriteLine("Main():   Press any key to exit..");
            Console.ReadLine();


        }
        /// <summary>
        /// Perform async query to my Product repository
        /// </summary>
        /// <returns></returns>
        public static async Task QueryingAsync()
        {
            using (var db = new ProductRepository())
            {

                db.ProductCategories.Add(new ProductCategory
                {
                    Description = "Hydraulic" + (db.ProductCategories.Count()).ToString(),
                    ModifiedDate = DateTime.Now
                });

                Console.WriteLine("QueryingAsync():   saving changes, second task is started");
                await db.SaveChangesAsync(); //Start new thread
                Console.WriteLine("QueryingAsync():   changes saved");

                Console.WriteLine("QueryingAsync():   reading data");
                var productCategories = await (from pc in db.ProductCategories
                                               select pc).ToListAsync();

                Console.WriteLine();
                Console.WriteLine("QueryingAsync():   all categories:");
               
                Each<ProductCategory>(productCategories, i => Console.WriteLine(i.Description));

            }

        }

        /// <summary>
        /// Generic method: iterates a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="action"></param>
        public static void Each<T>(IList<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }
    }
}
