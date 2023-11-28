using CourceProject.Models;
using DBModels;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Filtration
{
    public class FiltreRequestForDb
    {
        public IQueryable<Material> MaterialsFilter(MaterialsSuplyContext context, List<string> listOffilters)
        {
            IQueryable<Material> materials = context.Materials;
            IQueryable<Material> emptyQueryable = Enumerable.Empty<Material>().AsQueryable();

            if (listOffilters.Count != 0 && !(int.Parse(listOffilters[0]) > materials.Count()))
            {
                if (!string.IsNullOrEmpty(listOffilters[0]) && int.Parse(listOffilters[0]) != 0)
                {
                    materials = materials.Where(x => x.MaterialId == int.Parse(listOffilters[0]));
                }

                if (!string.IsNullOrEmpty(listOffilters[1]))
                {
                    materials = materials.Where(x => x.MaterialType == listOffilters[1]);
                }

                if (!string.IsNullOrEmpty(listOffilters[2]))
                {
                    materials = materials.Where(x => x.NameOfStateStandart == listOffilters[2]);
                }

                if (!string.IsNullOrEmpty(listOffilters[3]))
                {
                    materials = materials.Where(x => x.StateStandart == listOffilters[3]);
                }

                if (!string.IsNullOrEmpty(listOffilters[4]))
                {
                    materials = materials.Where(x => x.MeasureOfMeasurement == listOffilters[4]);
                }

                return materials;
            }
            else if (!string.IsNullOrEmpty(listOffilters[0]) && !string.IsNullOrEmpty(listOffilters[1]) &&
                !string.IsNullOrEmpty(listOffilters[2]) && !string.IsNullOrEmpty(listOffilters[3]) && !string.IsNullOrEmpty(listOffilters[4]))
            {
                return materials;
            }
            else
            {
                return emptyQueryable;
            }
        }

        public IQueryable<Employe> EmployesFilter(MaterialsSuplyContext context, List<string> listOffilters)
        {
            IQueryable<Employe> employes = context.Employes;
            IQueryable<Employe> emptyQueryable = Enumerable.Empty<Employe>().AsQueryable();

            if (listOffilters.Count != 0 && !(int.Parse(listOffilters[0]) > employes.Count()))
            {
                if (!string.IsNullOrEmpty(listOffilters[0]) && int.Parse(listOffilters[0]) != 0)
                {
                    employes = employes.Where(x => x.EmployeeId == int.Parse(listOffilters[0]));
                }

                if (!string.IsNullOrEmpty(listOffilters[1]))
                {
                    employes = employes.Where(x => x.Post == listOffilters[1]);
                }

                if (!string.IsNullOrEmpty(listOffilters[2]))
                {
                    employes = employes.Where(x => x.FirstName == listOffilters[2]);
                }

                if (!string.IsNullOrEmpty(listOffilters[3]))
                {
                    employes = employes.Where(x => x.LastName == listOffilters[3]);
                }

                return employes;
            }
            else if (!string.IsNullOrEmpty(listOffilters[0]) && !string.IsNullOrEmpty(listOffilters[1]) &&
                !string.IsNullOrEmpty(listOffilters[2]) && !string.IsNullOrEmpty(listOffilters[3]))
            {
                return employes;
            }
            else
            {
                return emptyQueryable;
            }
        }

        public IQueryable<DeliveredResource> DeliveredResourceFilter(MaterialsSuplyContext context, List<string> listOffilters)
        {
            IQueryable<DeliveredResource> deliveredResource = context.DeliveredResources;
            IQueryable<DeliveredResource> emptyQueryable = Enumerable.Empty<DeliveredResource>().AsQueryable();

            if (listOffilters.Count != 0 && !(int.Parse(listOffilters[0]) > deliveredResource.Count()))
            {
                if (!string.IsNullOrEmpty(listOffilters[0]) && int.Parse(listOffilters[0]) != 0)
                {
                    deliveredResource = deliveredResource.Where(x => x.DeliveredResourcesId == int.Parse(listOffilters[0]));
                }

                if (!string.IsNullOrEmpty(listOffilters[1]) && int.Parse(listOffilters[1]) > 0)
                {
                    deliveredResource = deliveredResource.Where(x => x.YearOfDelivery == int.Parse(listOffilters[1]));
                }

                if (!string.IsNullOrEmpty(listOffilters[2]) && int.Parse(listOffilters[2]) > 0 && int.Parse(listOffilters[0]) < 5)
                {
                    deliveredResource = deliveredResource.Where(x => x.QuarterOfDelivery == int.Parse(listOffilters[2]));
                }

                if (!string.IsNullOrEmpty(listOffilters[3]) && double.Parse(listOffilters[3]) != 0)
                {
                    deliveredResource = deliveredResource.Where(x => x.SizeOfResourseUsed >= double.Parse(listOffilters[3]));
                }

                if (!string.IsNullOrEmpty(listOffilters[4]) && int.Parse(listOffilters[4]) != 0)
                {
                    deliveredResource = deliveredResource.Where(x => x.SupplyContractsId == int.Parse(listOffilters[4]));
                }


                return deliveredResource;
            }
            else if (!string.IsNullOrEmpty(listOffilters[0]) && !string.IsNullOrEmpty(listOffilters[1]) &&
                !string.IsNullOrEmpty(listOffilters[2]) && !string.IsNullOrEmpty(listOffilters[3]) && !string.IsNullOrEmpty(listOffilters[4]))
            {
                return deliveredResource;
            }
            else
            {
                return emptyQueryable;
            }
        }

        public IQueryable<SupplyContract> SupplyContractFilter(MaterialsSuplyContext context, List<string> listOffilters)
        {
            IQueryable<SupplyContract> supplyContract = context.SupplyContracts;
            IQueryable<SupplyContract> emptyQueryable = Enumerable.Empty<SupplyContract>().AsQueryable();
            
            if (listOffilters.Count != 0 && !(int.Parse(listOffilters[0]) > supplyContract.Count()))
            {
                if (!string.IsNullOrEmpty(listOffilters[0]) && int.Parse(listOffilters[0]) != 0)
                {
                    supplyContract = supplyContract.Where(x => x.SupplyContractsId == int.Parse(listOffilters[0]));
                }

                if (DateTime.TryParse(listOffilters[1], out DateTime result_1) && DateTime.MinValue != result_1)
                {
                    supplyContract = supplyContract.Where(x => x.DateOfConclusion == DateTime.Parse(listOffilters[1]));
                }

                if (DateTime.TryParse(listOffilters[2], out DateTime result_2) && DateTime.MinValue != result_2)
                {
                    supplyContract = supplyContract.Where(x => x.DateOfDiliver == DateTime.Parse(listOffilters[2]));
                }

                if (!string.IsNullOrEmpty(listOffilters[3]))
                {
                    supplyContract = supplyContract.Where(x => x.Supplyer == (listOffilters[3]));
                }

                if (!string.IsNullOrEmpty(listOffilters[4]))
                {
                    supplyContract = supplyContract.Where(x => x.DiliverySize >= double.Parse(listOffilters[4]));
                }

                if (!string.IsNullOrEmpty(listOffilters[5]) && int.Parse(listOffilters[5]) != 0)
                {
                    supplyContract = supplyContract.Where(x => x.MaterialId == int.Parse(listOffilters[5]));
                }

                if (!string.IsNullOrEmpty(listOffilters[6]) && int.Parse(listOffilters[6]) != 0)
                {
                    supplyContract = supplyContract.Where(x => x.EmployeeId == int.Parse(listOffilters[6]));
                }
                return supplyContract;
            }
            else if (!string.IsNullOrEmpty(listOffilters[0]) && !string.IsNullOrEmpty(listOffilters[1]) &&
                !string.IsNullOrEmpty(listOffilters[2]) && !string.IsNullOrEmpty(listOffilters[3]) && !string.IsNullOrEmpty(listOffilters[4]))
            {
                return supplyContract;
            }
            else
            {
                return emptyQueryable;
            }
        }

        public IQueryable<RequiredResource> RequiredResourcesFilter(MaterialsSuplyContext context, List<string> listOffilters)
        {
            IQueryable<RequiredResource> requiredResource = context.RequiredResources;
            IQueryable<RequiredResource> emptyQueryable = Enumerable.Empty<RequiredResource>().AsQueryable();

            if (listOffilters.Count != 0 && !(int.Parse(listOffilters[0]) > requiredResource.Count()))
            {
                if (!string.IsNullOrEmpty(listOffilters[0]) && int.Parse(listOffilters[0]) != 0)
                {
                    requiredResource = requiredResource.Where(x => x.RequiredResourcesId == int.Parse(listOffilters[0]));
                }

                if (!string.IsNullOrEmpty(listOffilters[1]) && int.Parse(listOffilters[1]) > 0)
                {
                    requiredResource = requiredResource.Where(x => x.MaterialId == int.Parse(listOffilters[1]));
                }

                if (!string.IsNullOrEmpty(listOffilters[2]) && int.Parse(listOffilters[2]) > 0)
                {
                    requiredResource = requiredResource.Where(x => x.Year == int.Parse(listOffilters[2]));
                }

                if (!string.IsNullOrEmpty(listOffilters[3]) && int.Parse(listOffilters[3]) > 0 && int.Parse(listOffilters[3]) < 5 )
                {
                    requiredResource = requiredResource.Where(x => x.Quarter >= double.Parse(listOffilters[3]));
                }

                if (!string.IsNullOrEmpty(listOffilters[4]) && double.Parse(listOffilters[4]) != 0)
                {
                    requiredResource = requiredResource.Where(x => x.SizeOfResurces == double.Parse(listOffilters[4]));
                }


                return requiredResource;
            }
            else if (!string.IsNullOrEmpty(listOffilters[0]) && !string.IsNullOrEmpty(listOffilters[1]) &&
                !string.IsNullOrEmpty(listOffilters[2]) && !string.IsNullOrEmpty(listOffilters[3]) && !string.IsNullOrEmpty(listOffilters[4]))
            {
                return requiredResource;
            }
            else
            {
                return emptyQueryable;
            }
        }

        public async Task<IQueryable<User>> UserFilter(UserManager<User> context, List<string> listOffilters, string[] SelectRoles)
        {
            IQueryable<User> users = context.Users;
            IQueryable<User> emptyQueryable = Enumerable.Empty<User>().AsQueryable();

            if (listOffilters.Count != 0)
            {
                if (!string.IsNullOrEmpty(listOffilters[0]))
                {
                    users = users.Where(x => x.Id == listOffilters[0]);
                }

                if (!string.IsNullOrEmpty(listOffilters[1]))
                {
                    users = users.Where(x => x.FirstName == listOffilters[1]);
                }

                if (!string.IsNullOrEmpty(listOffilters[2]))
                {
                    users = users.Where(x => x.LastName == listOffilters[2]);
                }

                if (!string.IsNullOrEmpty(listOffilters[3]))
                {
                    users = users.Where(x => x.UserName == listOffilters[3]);
                }

                if (!string.IsNullOrEmpty(listOffilters[4]))
                {
                    users = users.Where(x => x.PhoneNumber == listOffilters[4]);
                }

                if (!string.IsNullOrEmpty(listOffilters[5]))
                {
                    users = users.Where(x => x.Email == listOffilters[5]);
                }

                if (SelectRoles.Length != 0)
                {
                    var prom = users.ToList();
                    var filteredUsers = prom.Where(u =>
                    {
                        var roles = context.GetRolesAsync(u).Result;
                        return roles.Intersect(SelectRoles).Any();
                    }).AsQueryable();
                    users = filteredUsers;
                }

                return users;
            }
            else if (!string.IsNullOrEmpty(listOffilters[0]) && !string.IsNullOrEmpty(listOffilters[1]) &&
                !string.IsNullOrEmpty(listOffilters[2]) && !string.IsNullOrEmpty(listOffilters[3]) && !string.IsNullOrEmpty(listOffilters[4]))
            {
                return users;
            }
            else
            {
                return emptyQueryable;
            }
        }
    }
}