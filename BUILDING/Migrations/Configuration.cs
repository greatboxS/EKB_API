namespace BUILDING.Migrations
{
    using SYS_MODELS;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BUILDING.BuildingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BUILDING.BuildingContext context)
        {
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
    }
}
