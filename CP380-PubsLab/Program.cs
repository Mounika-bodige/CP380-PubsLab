using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CP380_PubsLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbcontext = new Models.PubsDbContext())
            { 
                if (dbcontext.Database.CanConnect())
                {
                    Console.WriteLine("Yes, I can connect");
                }
                List<Models.Jls> det = new List<Models.Jls>();
                List<Models.Els> emp = new List<Models.Els>();
                foreach (var b in dbcontext.Jobs)
                {
                    det.Add(new Models.Jls() { Desc = b.Desc, Id = b.Id});
                }
              
                foreach (var b in dbcontext.Employee)
                {
                    emp.Add(new Models.Els() { Fname = b.Fname, Lname = b.Lname, jid = b.Job_Id });
                }

                foreach (var a in dbcontext.Employee)
                {
                    foreach (var b in det)
                    {
                        if (b.Id == a.Job_Id)
                        {
                          Console.WriteLine(a.Id+"   "+b.Desc);
                        }
                       
                    }

                }

                foreach (var a in dbcontext.Jobs)
                {
                    Console.WriteLine(a.Id);
                    foreach (var b in emp)
                    {
                        if (b.jid == a.Id)
                        {
                            Console.WriteLine(b.Fname+" "+b.Lname);
                        }

                    }

                }


                List<Models.Sls> sal = new List<Models.Sls>();
                List<Models.Tls> Titledata = new List<Models.Tls>();


                foreach (var b in dbcontext.Titles.Include(a => a.Salesli))
                {
                    sal.Add(new Models.Sls() { Salesid = b.Salesli.SId, title = b.title });
                }
                foreach (var b in dbcontext.Stores.Include(a => a.Salesli))
                {
                    Titledata.Add(new Models.Tls() { Salesid = b.Salesli.TId, name = b.name });
                }

                foreach (var a in dbcontext.Stores)
                {
                    Console.WriteLine(a.name);
                    foreach (var b in sal)
                    {
                        if (b.Salesid == a.Salesli.SId)
                        {
                            Console.WriteLine(b.title);
                        }

                    }
                }

                foreach (var a in dbcontext.Titles)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine(a.title);
                    foreach (var b in Titledata)
                    {
                        if (b.Salesid == a.Id)
                        {
                            Console.WriteLine(b.name);
                        }

                    }
                }

                
            }
        }
    }
}
