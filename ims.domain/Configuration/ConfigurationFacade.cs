using ims.data;
using ims.data.Entities;
using ims.domain.Infrastructure;
using ims.domain.Infrastructure.Architecture;
using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Configuration
{
    public interface IConfigurationFacade : IiMSFacade
    {
        void SetConfiguration(IMSConfiguration Config);
        void SetSession(UserSession session);
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
        Color UpdateColor(Color clr);
        Factory UpdateFactory(Factory fact);
        MachineCode UpdateMachineCode(MachineCode mc);
        Shop UpdateShop(Shop shop);
        Size UpdateSize(Size size);
        Sole UpdateSole(Sole sole);
        Store UpdateStore(Store store);
        BaseData GetInitDataModel();

        Supplier AddSupplier(Supplier suplier);
        Supplier UpdateSupplier(Supplier supplier);
        void DeleteSupplier(int id);
        Supplier GetSupplier(int id);
        List<Supplier> GetAllSuppliers();
    }

    public class ConfigurationFacade : IMSFacade, IConfigurationFacade
    {
        private IConfigurationService _confService;
        private StoreDbContext _context;
        private UserSession _session;
        private IMSConfiguration _config;
        public ConfigurationFacade(StoreDbContext Context, IConfigurationService confService) : base(Context)
        {
            _context = Context;
            _confService = confService;
            _confService.SetContext(Context);
        }

        public void SetSession(UserSession session)
        {
            _session = session;
            _confService.SetSession(session);

        }

        public void SetConfiguration(IMSConfiguration config)
        {
            _config = config;
            _confService.SetConfiguration(config);
        }

        public Color AddColor(Color clr)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.AddColor(clr);
            });
        }

        public void DeleteColor(int id)
        {
            Transact(t =>
            {
                PassContext(_confService, _context);
                _confService.DeleteColor(id);
            });
        }

        public List<Color> GetAllColors()
        {
            PassContext(_confService, _context);
            return _confService.GetAllColors();
        }

        public Color GetColor(int id)
        {
            PassContext(_confService, _context);
            return _confService.GetColor(id);
        }

        public Color UpdateColor(Color clr)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.UpdateColor(clr);
            });
        }

        public Store AddStore(Store str)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.AddStore(str);
            });
        }

        public void DeleteStore(int id)
        {
            Transact(t =>
            {
                PassContext(_confService, _context);
                _confService.DeleteStore(id);
            });
        }

        public List<Store> GetAllStores()
        {
            PassContext(_confService, _context);
            return _confService.GetAllStores();
        }

        public Store GetStore(int id)
        {
            PassContext(_confService, _context);
            return _confService.GetStore(id);
        }

        public Store UpdateStore(Store str)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.UpdateStore(str);
            });
        }

        public Factory AddFactory(Factory fact)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.AddFactory(fact);
            });
        }

        public void DeleteFactory(int id)
        {
            Transact(t =>
            {
                PassContext(_confService, _context);
                _confService.DeleteFactory(id);
            });
        }

        public List<Factory> GetAllFactories()
        {
            PassContext(_confService, _context);
            return _confService.GetAllFactories();
        }

        public Factory GetFactory(int id)
        {
            PassContext(_confService, _context);
            return _confService.GetFactory(id);
        }

        public Factory UpdateFactory(Factory fact)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.UpdateFactory(fact);
            });
        }

        public Sole AddSole(Sole sole)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.AddSole(sole);
            });
        }

        public void DeleteSole(int id)
        {
            Transact(t =>
            {
                PassContext(_confService, _context);
                _confService.DeleteSole(id);
            });
        }

        public List<Sole> GetAllSoles()
        {
            PassContext(_confService, _context);
            return _confService.GetAllSoles();
        }

        public Sole GetSole(int id)
        {
            PassContext(_confService, _context);
            return _confService.GetSole(id);
        }

        public Sole UpdateSole(Sole sole)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.UpdateSole(sole);
            });
        }

        public Size AddSize(Size size)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.AddSize(size);
            });
        }

        public void DeleteSize(int id)
        {
            Transact(t =>
            {
                PassContext(_confService, _context);
                _confService.DeleteSize(id);
            });
        }

        public List<Size> GetAllSizes()
        {
            PassContext(_confService, _context);
            return _confService.GetAllSizes();
        }

        public Size GetSize(int id)
        {
            PassContext(_confService, _context);
            return _confService.GetSize(id);
        }

        public Size UpdateSize(Size size)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.UpdateSize(size);
            });
        }

        public Shop AddShop(Shop shop)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.AddShop(shop);
            });
        }

        public void DeleteShop(int id)
        {
            Transact(t =>
            {
                PassContext(_confService, _context);
                _confService.DeleteShop(id);
            });
        }

        public List<Shop> GetAllShops()
        {
            PassContext(_confService, _context);
            return _confService.GetAllShops();
        }

        public Shop GetShop(int id)
        {
            PassContext(_confService, _context);
            return _confService.GetShop(id);
        }

        public Shop UpdateShop(Shop shop)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.UpdateShop(shop);
            });
        }

        public MachineCode AddMachineCode(MachineCode mc)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.AddMachineCode(mc);
            });
        }

        public void DeleteMachineCode(int id)
        {
            Transact(t =>
            {
                PassContext(_confService, _context);
                _confService.DeleteMachineCode(id);
            });
        }

        public List<MachineCode> GetAllMachineCodes()
        {
            PassContext(_confService, _context);
            return _confService.GetAllMachineCodes();
        }

        public MachineCode GetMachineCode(int id)
        {
            PassContext(_confService, _context);
            return _confService.GetMachineCode(id);
        }

        public MachineCode UpdateMachineCode(MachineCode mc)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.UpdateMachineCode(mc);
            });
        }

        public BaseData GetInitDataModel()
        {
            PassContext(_confService, _context);
            BaseData b = new BaseData()
            {
                Colors = _confService.GetAllColors().ToArray(),
                Factories = _confService.GetAllFactories().ToArray(),
                MachineCodes = _confService.GetAllMachineCodes().ToArray(),
                Shops = _confService.GetAllShops().ToArray(),
                Sizes = _confService.GetAllSizes().ToArray(),
                Soles = _confService.GetAllSoles().ToArray(),
                Stores = _confService.GetAllStores().ToArray(),
                Suppliers = _confService.GetAllSuppliers().ToArray()
            };
            return b;
        }

        public Supplier AddSupplier(Supplier suplier)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.AddSupplier(suplier);
            });
        }

        public Supplier UpdateSupplier(Supplier supplier)
        {
            return Transact(t =>
            {
                PassContext(_confService, _context);
                return _confService.UpdateSupplier(supplier);
            });
        }

        public void DeleteSupplier(int id)
        {
            Transact(t =>
            {
                PassContext(_confService, _context);
                _confService.DeleteSupplier(id);
            });
        }

        public Supplier GetSupplier(int id)
        {
            PassContext(_confService, _context);
            return _confService.GetSupplier(id);
        }

        public List<Supplier> GetAllSuppliers()
        {
            PassContext(_confService, _context);
            return _confService.GetAllSuppliers();
        }
    }
}
