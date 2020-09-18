using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Data;

namespace Persistence
{
    public static class Seed
    {
        public static void SeedData(DataContext context)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512();
            var hashKey = hmac.Key;
            var hashPass = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Password"));
            if (!context.Weapon_Tbl.Any())
            {
                var weapons = new List<Weapon>
                {
                    new Weapon
                    {
                        Name = "Longsword",
                        Gold = 100,
                        MinDamage = 3,
                        MaxDamage = 5,
                        MaxDurability = 5
                    },
                    new Weapon
                    {
                        Name = "Dagger",
                        Gold = 10,
                        MinDamage = 1,
                        MaxDamage = 3,
                        MaxDurability = 5
                    }
                };
                context.Weapon_Tbl.AddRange(weapons);
            }
            if (!context.Shield_Tbl.Any())
            {
                var shields = new List<Shield>
                {
                    new Shield
                    {
                        Name = "Buckler",
                        Gold = 100,
                        ArmorRating = 1,
                        MaxDurability = 3
                    },
                    new Shield
                    {
                        Name = "Tower Shield",
                        Gold = 200,
                        ArmorRating = 5,
                        MaxDurability = 5
                    }
                };
                context.Shield_Tbl.AddRange(shields);
            }

            if (!context.Potion_Tbl.Any())
            {
                var potions = new List<Potion>
                {
                    new Potion
                    {
                        Name = "Lesser Healing Potion",
                        Gold = 10,
                        Heal = 4
                    },
                    new Potion
                    {
                        Name = "Healing Potion",
                        Gold = 20,
                        Heal = 8
                    },
                    new Potion
                    {
                        Name = "Greater Healing Potion",
                        Gold = 35,
                        Heal = 15
                    }
                };
                context.Potion_Tbl.AddRange(potions);
            }

            if (!context.ItemType_Tbl.Any())
            {
                var itemTypes = new List<ItemType>
                {
                    new ItemType
                    {
                        TypeName = "Weapon"
                    },
                    new ItemType
                    {
                        TypeName = "Shield"
                    },
                    new ItemType
                    {
                        TypeName = "Potion"
                    }
                };
                context.ItemType_Tbl.AddRange(itemTypes);
            }
            if (!context.Users_Tbl.Any())
            {
                var dummyUsers = new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "Dummy User 1",
                        PasswordHash = hashPass,
                        PasswordSalt = hashKey,
                        Player = new Player
                        {
                            Max_HP = 1,
                            HP = 1,
                            XP = 1,
                            Gold = 1,
                            Level = 1,
                            Strength = 1,
                            Dexterity = 1,
                            Intelligence = 1,
                            Items = new List<ItemData>
                            {
                                new ItemData
                                {
                                    TypeReferenceId = 1,
                                    SubTypeReferenceId = 1,
                                    Container = ItemData.ContainerType.Equipment
                                },
                                new ItemData
                                {
                                    TypeReferenceId = 1,
                                    SubTypeReferenceId = 2,
                                    Container = ItemData.ContainerType.Equipment
                                },
                                new ItemData
                                {
                                    TypeReferenceId = 1,
                                    SubTypeReferenceId = 2,
                                    Container = ItemData.ContainerType.Inventory
                                },
                                new ItemData
                                {
                                    TypeReferenceId = 2,
                                    SubTypeReferenceId = 1,
                                    Container = ItemData.ContainerType.Inventory
                                },
                                new ItemData
                                {
                                    TypeReferenceId = 2,
                                    SubTypeReferenceId = 2,
                                    Container = ItemData.ContainerType.Inventory
                                },
                                                                new ItemData
                                {
                                    TypeReferenceId = 3,
                                    SubTypeReferenceId = 1,
                                    Container = ItemData.ContainerType.Inventory
                                },
                                new ItemData
                                {
                                    TypeReferenceId = 3,
                                    SubTypeReferenceId = 2,
                                    Container = ItemData.ContainerType.Inventory
                                },
                                                                new ItemData
                                {
                                    TypeReferenceId = 3,
                                    SubTypeReferenceId = 3,
                                    Container = ItemData.ContainerType.Inventory
                                }
                            }
                        }
                    }
                };
                context.Users_Tbl.AddRange(dummyUsers);
            }
            context.SaveChanges();

            hmac.Dispose(); // Free up HMAC object
        }
    }
}