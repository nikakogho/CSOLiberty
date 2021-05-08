using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSOLiberty.Models;
using Microsoft.EntityFrameworkCore;

namespace CSOLiberty.Data
{
    public static class DbInit
    {
        static Random random;

        static DateTime RandDate => DateTime.Now.AddYears(random.Next(-5, 5)).AddMonths(random.Next(1, 12)).AddDays(random.Next(1, 31));

        public static void Init(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Clients.Any() || context.Sellers.Any()) return;

            random = new Random();

            Client[] clients = 
            {
                new Client { FirstName = "Nika", LastName = "Koghuashvili" },
                new Client { FirstName = "Luka", LastName = "Tulashvili" },
                new Client { FirstName = "Gio", LastName = "Maziashvili" },
                new Client { FirstName = "John", LastName = "Rambo" },
                new Client { FirstName = "Some", LastName = "Guy" },
                new Client { FirstName = "Mary", LastName = "Jane" },
                new Client { FirstName = "Peter", LastName = "Parker" },
                new Client { FirstName = "Tom", LastName = "Holland" }
            };
            context.Clients.AddRange(clients);

            //for (int i = 0; i < clients.Length; i++) clients[i].ID = i + 1;

            Seller[] sellers =
            {
                new Seller { FirstName = "Eren", LastName = "Yaeger" },
                new Seller { FirstName = "Eren", LastName = "Kruger" },
                new Seller { FirstName = "Zeke", LastName = "Yaeger" },
                new Seller { FirstName = "Grisha", LastName = "Yaeger" },
                new Seller { FirstName = "Pieck", LastName = "Finger" },
                new Seller { FirstName = "Porco", LastName = "Galliard" },
                new Seller { FirstName = "Reiner", LastName = "Braun" },
                new Seller { FirstName = "Berthold", LastName = "Hoover" },
                new Seller { FirstName = "Annie", LastName = "Leonhart" }
            };
            //for (int i = 0; i < sellers.Length; i++) sellers[i].BossID = null;
            //context.AddRange(sellers);

            int[] sellerBosses = { 0, 0, 0, 1, 2, 2, 2, 6, 6 };
            for (int i = 0; i < sellers.Length; i++)
            {
                //sellers[i].ID = i + 1;
                context.Add(sellers[i]);
                if(sellerBosses[i] != i) sellers[i].Boss = sellers[sellerBosses[i]];
            }

            context.SaveChanges();

            Order[] orders =
            {
                new Order { Amount = 20, Date = RandDate },
                new Order { Amount = 15, Date = RandDate },
                new Order { Amount = 12, Date = RandDate },
                new Order { Amount = 25, Date = RandDate },
                new Order { Amount = 34, Date = RandDate },
                new Order { Amount = 16, Date = RandDate },
                new Order { Amount = .8, Date = RandDate },
                new Order { Amount = .3, Date = RandDate }
            };
            int[,] orderDetails = {
                { 0, 0, -1 }, // client index, seller index, parent index
                { 3, 4, -1 },
                { 2, 6, -1 },
                { 3, 4, -1 },
                { 0, 2, -1 },
                { 0, 2, -1 },
                { 0, 2, -1 },
                { 0, 2, 5 },
                { 0, 2, 5 }
            };
            for (int i = 0; i < orders.Length; i++)
            {
                //orders[i].ID = i + 1;

                orders[i].Client = clients[orderDetails[i, 0]];
                orders[i].Seller = sellers[orderDetails[i, 1]];

                context.Add(orders[i]);

                int parentIndex = orderDetails[i, 2];

                orders[i].Parent = parentIndex < 0 ? null : orders[parentIndex];
            }

            //foreach (var client in clients) context.Clients.Add(client);
            //foreach (var seller in sellers) context.Sellers.Add(seller);
            //foreach (var order in orders) context.Orders.Add(order);

            //context.Clients.AddRange(clients);
            //context.SaveChanges();

            //context.Sellers.AddRange(sellers);
            //context.SaveChanges();

            //context.Orders.AddRange(orders);

            context.SaveChanges();

            Console.WriteLine("Saved some stuff");
        }
    }
}
