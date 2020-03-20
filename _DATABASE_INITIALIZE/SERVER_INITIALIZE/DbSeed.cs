using BEAMCUT_TASK;
using BUILDING;
using COMPONENT;
using EKANBAN_HIS;
using EKANBAN_TASK;
using EMPLOYEE;
using SCHEDULE;
using SEQUENCE;
using STOCK_MANAGEMENT;
using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DATABASE_INITIALIZE.SERVER_INITIALIZE
{
    public class DbSeed
    {
        public IDbName Database { get; private set; }
        public DbSeed(IDbName _database)
        {
            Database = _database;
        }

        public void Runing()
        {
            Building();
            Employee();
            Schedule();
            EKanbanDevices();
            BeamCutDevices();
            Component();

            var EkanbanHisContext = new HistoryContext(Database);
            var sequenceContext = new SequenceContext(Database);
            var StockManagerContext = new StockContext(Database);

            if (!EkanbanHisContext.Database.Exists())
            {
                EkanbanHisContext.Database.Create();
                try
                {
                    EkanbanHisContext.Database.ExecuteSqlCommand("DROP TABLE [__MigrationHistory]");
                    EkanbanHisContext.SaveChanges();
                }
                catch { }
                Console.WriteLine("EkanbanHisContext is created successfully");
            }
            else
                Console.WriteLine("EkanbanHisContext is existed");


            if (!sequenceContext.Database.Exists())
            {
                sequenceContext.Database.Create();
                try
                {
                    sequenceContext.Database.ExecuteSqlCommand("DROP TABLE [__MigrationHistory]");
                    sequenceContext.SaveChanges();
                }
                catch { }
                Console.WriteLine("SequenceContext is created successfully");
            }
            else
                Console.WriteLine("sequenceContext is existed");

            if (!StockManagerContext.Database.Exists())
            {
                StockManagerContext.Database.Create();
                try
                {
                    StockManagerContext.Database.ExecuteSqlCommand("DROP TABLE [__MigrationHistory]");
                    StockManagerContext.SaveChanges();
                }
                catch { }
                Console.WriteLine("StockManagerContext is created successfully");
            }
            else
                Console.WriteLine("StockManagerContext is existed");
        }

        public void Employee()
        {
            using (var context = new EmployeeContext(Database))
            {
                if (context.Database.Exists())
                {
                    Console.WriteLine("EmployeeContext is existed");
                    return;
                }

                context.Database.Create();
                Console.WriteLine("EmployeeContext is created successfully");

                context.Positions.AddRange(new Position[] {
                new Position { PostionName = "Division Supervisor" },
                new Position { PostionName = "Building Supervisor" },
                new Position { PostionName = "Beam Worker" },
                new Position { PostionName = "Auto Worker" },
                new Position { PostionName = "Water Spider" }}
            );

                context.AppUsers.AddRange(new AppUser[] {
                new AppUser { user = "admin", password="admin", userClass=3 },
                new AppUser { user = "officer", password="officer", userClass=2 },
                new AppUser { user = "worker", password="123", userClass=1 }}
                );

                context.JobTitles.AddRange(new JobTitle[]
                {
                new JobTitle{ Job= "Chặt phụ kiện"},
                new JobTitle{ Job= "Chặt da"},
                });

                context.SaveChanges();

                context.Departments.AddRange(new Department[]
                {
                new Department{Code="A8L",},
                 new Department{Code="A8J",},
                });
                context.SaveChanges();


                context.Employees.AddRange(new Employee[]
                {
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Nguyễn Thị Ngọc Hương",
                    UserCode = 48798,
                    RFID_Code = "29CF654F",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Đặng Khắc Ghi",
                    UserCode = 49722,
                    RFID_Code = "0C31E1CB",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Lê Thị Kim Loan",
                    UserCode = 41270,
                    RFID_Code = "",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Đinh Thị Liên",
                    UserCode = 39385,
                    RFID_Code = "",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Đồng Thị Thùy",
                    UserCode = 48097,
                    RFID_Code = "",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Hồ Thị Ánh Nga ",
                    UserCode = 50598,
                    RFID_Code = "",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
                new Employee
                {
                    JobTitle_Id=1,
                    Name="Trần Kim Lý",
                    UserCode = 52211,
                    RFID_Code = "",
                    Department_Id =1,
                    Building_Id=1,
                    Position_Id = 3
                },
                });
                context.SaveChanges();

                try
                {
                    context.Database.ExecuteSqlCommand("DROP TABLE [__MigrationHistory]");

                    context.SaveChanges();
                }
                catch { }
            }
        }

        public void Building()
        {
            using (var context = new BuildingContext(Database))
            {
                if (context.Database.Exists())
                {
                    Console.WriteLine("BuildingContext is existing");
                    return;
                }

                context.Database.Create();
                Console.WriteLine("BuildingContext is created successfully");

                Building l = new Building
                {
                    Name = "L",
                    ProductionLines = new List<ProductionLine>()
                };

                for (int i = 1; i <= 6; i++)
                {
                    l.ProductionLines.Add(new ProductionLine { LineName = $"L{i}" });
                }
                context.Buildings.Add(l);

                Building E = new Building
                {
                    Name = "E",
                    ProductionLines = new List<ProductionLine>()
                };

                for (int i = 1; i <= 7; i++)
                {
                    E.ProductionLines.Add(new ProductionLine { LineName = $"E{i}" });
                }

                context.Buildings.Add(E);

                Building I = new Building
                {
                    Name = "I",
                    ProductionLines = new List<ProductionLine>()
                };

                for (int i = 1; i <= 11; i++)
                {
                    I.ProductionLines.Add(new ProductionLine { LineName = $"I{i}" });
                }

                context.Buildings.Add(I);

                Building O = new Building
                {
                    Name = "O",
                    ProductionLines = new List<ProductionLine>()
                };

                for (int i = 1; i <= 2; i++)
                {
                    O.ProductionLines.Add(new ProductionLine { LineName = $"O{i}" });
                }

                context.Buildings.Add(O);

                Building P = new Building
                {
                    Name = "P",
                    ProductionLines = new List<ProductionLine>()
                };

                for (int i = 1; i <= 7; i++)
                {
                    P.ProductionLines.Add(new ProductionLine { LineName = $"P{i}" });
                }

                context.Buildings.Add(P);

                Building M = new Building
                {
                    Name = "M",
                    ProductionLines = new List<ProductionLine>()
                };

                for (int i = 1; i <= 3; i++)
                {
                    M.ProductionLines.Add(new ProductionLine { LineName = $"M{i}" });
                }

                context.Buildings.Add(M);

                Building J = new Building
                {
                    Name = "J",
                    ProductionLines = new List<ProductionLine>()
                };

                for (int i = 1; i <= 3; i++)
                {
                    J.ProductionLines.Add(new ProductionLine { LineName = $"J{i}" });
                }
                context.Buildings.Add(J);
                context.SaveChanges();


                // add line O
                AddNewProductionLineName(context, "O", 2);
                // add line P
                AddNewProductionLineName(context, "P", 7);
                // add line I
                AddNewProductionLineName(context, "L", 7);
                // add line E
                AddNewProductionLineName(context, "I", 11);
                // add line L
                AddNewProductionLineName(context, "J", 3);
                // add line J
                AddNewProductionLineName(context, "M", 3);
                // add line L
                AddNewProductionLineName(context, "E", 7);

                try
                {
                    context.Database.ExecuteSqlCommand("DROP TABLE [__MigrationHistory]");

                    context.SaveChanges();
                }
                catch { }
            }
        }
        public void AddNewProductionLineName(BuildingContext context, string LineName, int count)
        {
            List<ProductionLineName> _lineName = new List<ProductionLineName>();

            if (LineName == "L")
            {
                for (int i = 0; i < count; i++)
                {
                    _lineName.Add(new ProductionLineName
                    {
                        SystemCode = $"L{i + 1}9",
                        LineCode = $"L{i + 1}",
                        DisplayCode = $"L{i + 1}",
                    });
                }

                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "LJ9",
                    LineCode = "L1A",
                    DisplayCode = "L1",
                });
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "LK9",
                    LineCode = "L1B",
                    DisplayCode = "L1",
                });
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "LH9",
                    LineCode = "L2A",
                    DisplayCode = "L2",
                });
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "LI9",
                    LineCode = "L2A",
                    DisplayCode = "L2",
                });
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "LL9",
                    LineCode = "L3A",
                    DisplayCode = "L3",
                });
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "LM9",
                    LineCode = "L3B",
                    DisplayCode = "L3",
                });
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "LN9",
                    LineCode = "L4A",
                    DisplayCode = "L4",
                });
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "LO9",
                    LineCode = "L4B",
                    DisplayCode = "L4",
                });
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "LP9",
                    LineCode = "L5A",
                    DisplayCode = "L5",
                });
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "LQ9",
                    LineCode = "L5B",
                    DisplayCode = "L5",
                });
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "LR9",
                    LineCode = "L6A",
                    DisplayCode = "L6",
                });
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "LT9",
                    LineCode = "L6B",
                    DisplayCode = "L6",
                });

                context.ProductionLineNames.AddRange(_lineName);
                context.SaveChanges();
                return;
            }

            if (LineName == "M")
            {
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "MA9", // begin H ->M
                    LineCode = "M1",
                    DisplayCode = "M1",
                });
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "MB9", // begin H ->M
                    LineCode = "M2",
                    DisplayCode = "M2",
                });
                _lineName.Add(new ProductionLineName
                {
                    SystemCode = "MC9", // begin H ->M
                    LineCode = "M3",
                    DisplayCode = "M3",
                });
            }

            //Line O
            for (int i = 0; i < count; i++)
            {
                if (LineName == "M")
                {
                    _lineName.Add(new ProductionLineName
                    {
                        SystemCode = $"M{(char)(i + 72)}9", // begin H ->M
                        LineCode = $"M{i + 1}A",
                        DisplayCode = $"M{i + 1}",
                    });
                    _lineName.Add(new ProductionLineName
                    {
                        SystemCode = $"M{(char)(i + 72 + 1)}9",
                        LineCode = $"M{i + 1}B",
                        DisplayCode = $"M{i + 1}",
                    });
                }
                else
                {
                    _lineName.Add(new ProductionLineName
                    {
                        SystemCode = $"{LineName}{(char)(i + 65)}9",
                        LineCode = $"{LineName}{i + 1}",
                        DisplayCode = $"{LineName}{i + 1}",
                    });
                }
            }
            context.ProductionLineNames.AddRange(_lineName);
            context.SaveChanges();
        }

        public void Schedule()
        {
            using (var context = new ScheduleContext(Database))
            {
                if (context.Database.Exists())
                {
                    Console.WriteLine("ScheduleContext is existing");
                    return;
                }

                context.Database.Create();
                Console.WriteLine("ScheduleContext is created successfully");

                try
                {
                    context.Database.ExecuteSqlCommand("DROP TABLE [__MigrationHistory]");

                    context.SaveChanges();
                }
                catch { }

                context.FilePaths.Add(new FilePath
                {
                    //Path = @"C:\Users\Dell-PC\OneDrive\RevolutionTwo\Schedule",
                    Path = @"\\svproxy\Department\CP\Planning Team\生管進度 - Production Schedule - Tiến độ của sinh quản",
                    Type = "schedule"
                });
                context.SaveChanges();


            }
        }

        public void EKanbanDevices()
        {
            using (var context = new EKanbanTaskContext(Database))
            {
                if (context.Database.Exists())
                    return;

                context.Database.Create();

                for (int i = 1; i <= 40; i++)
                {
                    context.EKanbanDevices.Add(new EKanbanDevice
                    {
                        Name = $"Cart{i}",
                        Building_Id = 1,
                        PropductionLine_Id = i / 5 + 1,
                    });

                    context.SaveChanges();
                }

                try
                {
                    context.Database.ExecuteSqlCommand("DROP TABLE [__MigrationHistory]");

                    context.SaveChanges();
                }
                catch { }
            }
        }

        public void BeamCutDevices()
        {
            using (var context = new BeamCutContext(Database))
            {
                if (context.Database.Exists())
                {
                    Console.WriteLine("BeamCutContext is existing");
                    return;
                }

                context.Database.Create();
                Console.WriteLine("BeamCutContext is created successfully");

                for (int i = 1; i <= 10; i++)
                {
                    context.BeamCutDevices.Add(new BeamCutDevice
                    {
                        Building_Id = 1,
                        Name = $"BC{i}",
                    });
                    context.SaveChanges();
                }

                try
                {
                    context.Database.ExecuteSqlCommand("DROP TABLE [__MigrationHistory]");

                    context.SaveChanges();
                }
                catch { }
            }
        }

        public void Component()
        {
            using (var context = new ShoeContext(Database))
            {
                if (context.Database.Exists())
                {
                    Console.WriteLine("ShoeContext is existing");
                    return;
                }

                context.Database.Create();
                Console.WriteLine("ShoeContext is created successfully");

                List<ShoeSize> sizes = new List<ShoeSize>();
                for (int i = 0; i <= 15; i++)
                {
                    sizes.AddRange(new ShoeSize[] {
                    new ShoeSize{SizeName=$"{i}" },
                    new ShoeSize{SizeName=$"{i}T"}
                    });
                }
                context.ShoeSizes.AddRange(sizes);
                context.SaveChanges();

                context.CuttingTypes.AddRange(new CuttingType[]
                {
                    new CuttingType{TypeName = "auto cut"},
                    new CuttingType{TypeName = "beam cut"}
                });
                context.SaveChanges();

                try
                {
                    context.Database.ExecuteSqlCommand("DROP TABLE [__MigrationHistory]");

                    context.SaveChanges();
                }
                catch { }
            }
        }
    }
}
