using ims.data;
using ims.data.Entities;
using ims.domain.Infrastructure;
using ims.domain.Infrastructure.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ims.domain.Configuration
{
    public class ConfigurationService :  IMSService , IConfigurationService
    {

        private StoreDbContext _context;
        private IMSConfiguration _config;
        private UserSession _session;

        public ConfigurationService(StoreDbContext context)
        {
            _context = context;
        }

        public void SetSession(UserSession session)
        {
            _session = session;
        }
        public void SetConfiguration(IMSConfiguration Config)
        {
            _config = Config;
            
        }

        public Store AddStore(Store store)
        {
            _context.Stores.Add(store);
            _context.SaveChanges();
            return store;
        }

        public Store UpdateStore(Store store)
        {
            var s = _context.Stores.Update(store);
            _context.SaveChanges();
            return store;
        }

        public void DeleteStore(int id)
        {
            var l = _context.Stores.Where(m => m.Id == id).First();
            _context.Stores.Remove(l);
            _context.SaveChanges();
        }

        public Store GetStore(int id)
        {
            return _context.Stores.Where(m => m.Id == id).First();
        }

        public List<Store> GetAllStores()
        {
            return _context.Stores.ToList();
        }

        public Factory AddFactory(Factory fact)
        {
            _context.Factories.Add(fact);
            _context.SaveChanges();
            return fact;
        }

        public Factory UpdateFactory(Factory fact)
        {
            var s = _context.Factories.Update(fact);
            _context.SaveChanges();
            return fact;
        }

        public void DeleteFactory(int id)
        {
            var l = _context.Factories.Where(m => m.FactoryId == id).First();
            _context.Factories.Remove(l);
            _context.SaveChanges();
        }

        public Factory GetFactory(int id)
        {
            return _context.Factories.Where(m => m.FactoryId == id).First();
        }

        public List<Factory> GetAllFactories()
        {
            return _context.Factories.ToList();
        }

        public Sole AddSole(Sole sole)
        {
            _context.Soles.Add(sole);
            _context.SaveChanges();
            return sole;
        }

        public Sole UpdateSole(Sole sole)
        {
            var s = _context.Soles.Update(sole);
            _context.SaveChanges();
            return sole;
        }

        public void DeleteSole(int id)
        {
            var l = _context.Soles.Where(m => m.SoleId == id).First();
            _context.Soles.Remove(l);
            _context.SaveChanges();
        }

        public Sole GetSole(int id)
        {
            return _context.Soles.Where(m => m.SoleId == id).First();
        }

        public List<Sole> GetAllSoles()
        {
            return _context.Soles.ToList();
        }

        public Size AddSize(Size size)
        {
            _context.Sizes.Add(size);
            _context.SaveChanges();
            return size;
        }

        public Size UpdateSize(Size size)
        {
            var s = _context.Sizes.Update(size);
            _context.SaveChanges();
            return size;
        }

        public void DeleteSize(int id)
        {
            var l = _context.Sizes.Where(m => m.SizeId == id).First();
            _context.Sizes.Remove(l);
            _context.SaveChanges();
        }

        public Size GetSize(int id)
        {
            return _context.Sizes.Where(m => m.SizeId == id).First();
        }

        public List<Size> GetAllSizes()
        {
            return _context.Sizes.ToList();
        }

        public Shop AddShop(Shop shop)
        {
            _context.Shops.Add(shop);
            _context.SaveChanges();
            return shop;
        }

        public Shop UpdateShop(Shop shop)
        {
            var s = _context.Shops.Update(shop);
            _context.SaveChanges();
            return shop;
        }

        public void DeleteShop(int id)
        {
            var l = _context.Shops.Where(m => m.ShopId == id).First();
            _context.Shops.Remove(l);
            _context.SaveChanges();
        }

        public Shop GetShop(int id)
        {
            return _context.Shops.Where(m => m.ShopId == id).First();
        }

        public List<Shop> GetAllShops()
        {
            return _context.Shops.ToList();
        }

        public MachineCode AddMachineCode(MachineCode mc)
        {
            _context.MachineCodes.Add(mc);
            _context.SaveChanges();
            return mc;
        }

        public MachineCode UpdateMachineCode(MachineCode mc)
        {
            var s = _context.MachineCodes.Update(mc);
            _context.SaveChanges();
            return mc;
        }

        public void DeleteMachineCode(int id)
        {
            var l = _context.MachineCodes.Where(m => m.MachineCodeId == id).First();
            _context.MachineCodes.Remove(l);
            _context.SaveChanges();
        }

        public MachineCode GetMachineCode(int id)
        {
            return _context.MachineCodes.Where(m => m.MachineCodeId == id).First();
        }

        public List<MachineCode> GetAllMachineCodes()
        {
            return _context.MachineCodes.ToList();
        }

        public Color AddColor(Color clr)
        {
            _context.Colors.Add(clr);
            _context.SaveChanges();
            return clr;
        }

        public Color UpdateColor(Color clr)
        {
            var s = _context.Colors.Update(clr);
            _context.SaveChanges();
            return clr;
        }

        public void DeleteColor(int id)
        {
            var l = _context.Colors.Where(m => m.ColorId == id).First();
            _context.Colors.Remove(l);
            _context.SaveChanges();
        }

        public Color GetColor(int id)
        {
            return _context.Colors.Where(m => m.ColorId == id).First();
        }

        public List<Color> GetAllColors()
        {
            return _context.Colors.ToList();
        }

        public Supplier AddSupplier(Supplier suplier)
        {
            _context.Suppliers.Add(suplier);
            _context.SaveChanges();
            return suplier;
        }
        
        public Supplier UpdateSupplier(Supplier supplier)
        {
            var s = _context.Suppliers.Update(supplier);
            _context.SaveChanges();
            return supplier;
        }

        public void DeleteSupplier(int id)
        {
            var supp = _context.Suppliers.Where(m => m.ID == id).First();
            _context.Suppliers.Remove(supp);
            _context.SaveChanges();
        }

        public Supplier GetSupplier(int id)
        {
            var supp = _context.Suppliers.Find(id);
            return supp;
        }

        public List<Supplier> GetAllSuppliers()
        {
            return _context.Suppliers.ToList();
        }


    }
}
