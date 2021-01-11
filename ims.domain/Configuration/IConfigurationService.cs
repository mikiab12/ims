using System.Collections.Generic;
using ims.data.Entities;
using ims.domain.Infrastructure;
using ims.domain.Infrastructure.Architecture;

namespace ims.domain.Configuration
{
    public interface IConfigurationService : iIMSService
    {
        Color AddColor(Color clr);
        Factory AddFactory(Factory fact);
        MachineCode AddMachineCode(MachineCode mc);
        Shop AddShop(Shop shop);
        Size AddSize(Size size);
        Sole AddSole(Sole sole);
        Store AddStore(Store store);
        void DeleteColor(int id);
        void DeleteFactory(int id);
        void DeleteMachineCode(int id);
        void DeleteShop(int id);
        void DeleteSize(int id);
        void DeleteSole(int id);
        void DeleteStore(int id);
        List<Color> GetAllColors();
        List<Factory> GetAllFactories();
        List<MachineCode> GetAllMachineCodes();
        List<Shop> GetAllShops();
        List<Size> GetAllSizes();
        List<Sole> GetAllSoles();
        List<Store> GetAllStores();
        Color GetColor(int id);
        Factory GetFactory(int id);
        MachineCode GetMachineCode(int id);
        Shop GetShop(int id);
        Size GetSize(int id);
        Sole GetSole(int id);
        Store GetStore(int id);
        void SetConfiguration(IMSConfiguration Config);
        void SetSession(UserSession session);
        Color UpdateColor(Color clr);
        Factory UpdateFactory(Factory fact);
        MachineCode UpdateMachineCode(MachineCode mc);
        Shop UpdateShop(Shop shop);
        Size UpdateSize(Size size);
        Sole UpdateSole(Sole sole);
        Store UpdateStore(Store store);
        Supplier AddSupplier(Supplier suplier);
        Supplier UpdateSupplier(Supplier supplier);
        void DeleteSupplier(int id);
        Supplier GetSupplier(int id);
        List<Supplier> GetAllSuppliers();
    }
}