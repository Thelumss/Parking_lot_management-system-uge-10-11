using Microsoft.EntityFrameworkCore;
using Parking_lot_management_system_uge_10_11.Data;

namespace Parking_lot_management_system_uge_10_11.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<DataContext>();

            {
                if (!context.user_Types.Any())
                {
                    context.AddRange(new User_Types
                    {
                        User_TypesName = "Admin"
                    });

                    context.AddRange(new User_Types
                    {
                        User_TypesName = "Company_Admin"
                    });

                }

                context.SaveChanges();

                if (!context.Organisation.Any())
                {
                    context.AddRange(new Organisation
                    {
                        OrganisationName = "CreatringCompany"
                    });

                    context.AddRange(new Organisation
                    {
                        OrganisationName = "RosegardenParking"
                    });
                    context.AddRange(new Organisation
                    {
                        OrganisationName = "OdenseCityParking"
                    });

                }

                context.SaveChanges();

                if (!context.Users.Any())
                {
                    context.AddRange(new Users
                    {
                        Name = "admin",
                        Password = "admin",
                        Email = "admin@admin",
                        UserTypeID = context.user_Types
                            .First(p => p.User_TypesName == "Admin").User_TypesID,
                        OrganisationId = context.Organisation
                            .First(p => p.OrganisationName == "CreatringCompany").OrganisationID,

                    });

                    context.AddRange(new Users
                    {
                        Name = "RosegardenParking",
                        Password = "RosegardenParking",
                        Email = "RosegardenParking@email",
                        UserTypeID = context.user_Types
                            .First(p => p.User_TypesName == "Company_Admin").User_TypesID,
                        OrganisationId = context.Organisation
                            .First(p => p.OrganisationName == "RosegardenParking").OrganisationID,

                    });


                    context.AddRange(new Users
                    {
                        Name = "OdenseCityParking",
                        Password = "OdenseCityParking",
                        Email = "OdenseCityParking@email",
                        UserTypeID = context.user_Types
                            .First(p => p.User_TypesName == "Admin").User_TypesID,
                        OrganisationId = context.Organisation
                            .First(p => p.OrganisationName == "OdenseCityParking").OrganisationID,

                    });

                }

                context.SaveChanges();

                if (!context.lot_Types.Any())
                {
                    context.AddRange(new Lot_types
                    {
                        Type = "standard",
                        Price_Multiplier = 1,
                    });

                    context.AddRange(new Lot_types
                    {
                        Type = "EV",
                        Price_Multiplier = 1,
                    });
                    context.AddRange(new Lot_types
                    {
                        Type = "handicapped",
                        Price_Multiplier = 1,
                    });

                }

                context.SaveChanges();

                if (!context.parking_Lot_Structurs.Any())
                {
                    context.AddRange(new Parking_Lot_Structur
                    {
                        Name = "rosegarden lots",
                        Adress = "smoethingstreet",
                        BasePrice = 200,
                        Total_Available_Lots = 200,
                        Total_Occupied_Lots = 0,
                        OrganisationId = context.Organisation
                            .First(p => p.OrganisationName == "RosegardenParking").OrganisationID,
                    });

                    context.AddRange(new Parking_Lot_Structur
                    {
                        Name = "city garage A",
                        Adress = "smoethingstreet",
                        BasePrice = 200,
                        Total_Available_Lots = 100,
                        Total_Occupied_Lots = 0,
                        OrganisationId = context.Organisation
                            .First(p => p.OrganisationName == "OdenseCityParking").OrganisationID,
                    });
                    context.AddRange(new Parking_Lot_Structur
                    {
                        Name = "city garage B",
                        Adress = "smoethingstreet",
                        BasePrice = 200,
                        Total_Available_Lots = 50,
                        Total_Occupied_Lots = 0,
                        OrganisationId = context.Organisation
                            .First(p => p.OrganisationName == "OdenseCityParking").OrganisationID,
                    });
                }

                context.SaveChanges();

                if (!context.lots.Any())
                {
                        // Cache IDs BEFORE loops
                        var structRosegarden = context.parking_Lot_Structurs
                            .First(p => p.Name == "rosegarden lots").Parking_lot_Structur_ID;

                        var structGarageA = context.parking_Lot_Structurs
                            .First(p => p.Name == "city garage A").Parking_lot_Structur_ID;

                        var structGarageB = context.parking_Lot_Structurs
                            .First(p => p.Name == "city garage B").Parking_lot_Structur_ID;

                        var typeStandard = context.lot_Types.First(t => t.Type == "standard").Lot_typesID;
                        var typeEV = context.lot_Types.First(t => t.Type == "EV").Lot_typesID;
                        var typeHandicap = context.lot_Types.First(t => t.Type == "handicapped").Lot_typesID;

                        // Rosegarden – standard
                        for (int i = 0; i < 175; i++)
                        {
                            context.lots.Add(new Lot
                            {
                                Structur_ID = structRosegarden,
                                LotName = Convert.ToString(i+1),
                                Lot_types_ID = typeStandard,
                                Occupied_Status = false
                            });
                        }

                        // Rosegarden – EV
                        for (int i = 175; i < 180; i++)
                        {
                            context.lots.Add(new Lot
                            {
                                Structur_ID = structRosegarden,
                                LotName = Convert.ToString(i + 1),
                                Lot_types_ID = typeEV,
                                Occupied_Status = false
                            });
                        }

                        // Rosegarden – handicapped
                        for (int i = 180; i < 200; i++)
                        {
                            context.lots.Add(new Lot
                            {
                                Structur_ID = structRosegarden,
                                LotName = Convert.ToString(i + 1),
                                Lot_types_ID = typeHandicap,
                                Occupied_Status = false
                            });
                        }

                        // Garage A – standard
                        for (int i = 0; i < 75; i++)
                        {
                            context.lots.Add(new Lot
                            {
                                Structur_ID = structGarageA,
                                LotName = Convert.ToString(i + 1),
                                Lot_types_ID = typeStandard,
                                Occupied_Status = false
                            });
                        }

                        // Garage A – EV
                        for (int i = 75; i < 80; i++)
                        {
                            context.lots.Add(new Lot
                            {
                                Structur_ID = structGarageA,
                                LotName = Convert.ToString(i + 1),
                                Lot_types_ID = typeEV,
                                Occupied_Status = false
                            });
                        }

                        // Garage A – handicapped
                        for (int i = 80; i < 100; i++)
                        {
                            context.lots.Add(new Lot
                            {
                                Structur_ID = structGarageA,
                                LotName = Convert.ToString(i + 1),
                                Lot_types_ID = typeHandicap,
                                Occupied_Status = false
                            });
                        }

                        // Garage B – standard
                        for (int i = 0; i < 45; i++)
                        {
                            context.lots.Add(new Lot
                            {
                                Structur_ID = structGarageB,
                                LotName = Convert.ToString(i + 1),
                                Lot_types_ID = typeStandard,
                                Occupied_Status = false
                            });
                        }

                        // Garage B – handicapped
                        for (int i = 45; i < 50; i++)
                        {
                            context.lots.Add(new Lot
                            {
                                Structur_ID = structGarageB,
                                LotName = Convert.ToString(i + 1),
                                Lot_types_ID = typeHandicap,
                                Occupied_Status = false
                            });
                        }

                    }

                    context.SaveChanges();

                if (!context.lot_Histories.Any())
                {
                    var firstLot = context.lots
                        .Where(p => !p.Occupied_Status)
                        .OrderBy(p => p.LotID)
                        .FirstOrDefault();

                    var lastLot = context.lots
                        .Where(p => !p.Occupied_Status)
                        .OrderByDescending(p => p.LotID)
                        .FirstOrDefault();


                    if (firstLot != null && lastLot != null)
                    {
                        context.lot_Histories.AddRange(
                            new Lot_History
                            {
                                Lot_ID = firstLot.LotID,
                                License_PLate_Numbers = "DKdwaff35",
                                Entry_time = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                                Exit_time = DateTimeOffset.Now.AddHours(2).ToUnixTimeMilliseconds(),
                            },
                            new Lot_History
                            {
                                Lot_ID = lastLot.LotID,
                                License_PLate_Numbers = "DKjytu35",
                                Entry_time = DateTimeOffset.Now.AddHours(1).ToUnixTimeMilliseconds(),
                                Exit_time = DateTimeOffset.Now.AddHours(3).ToUnixTimeMilliseconds(),
                            }
                        );

                        context.SaveChanges();
                    }
                }

            }
        }
        }
    }


