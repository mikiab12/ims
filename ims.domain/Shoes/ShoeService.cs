using ims.data;
using ims.data.Entities;
using ims.domain.Infrastructure;
using ims.domain.Infrastructure.Architecture;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ims.domain.Shoes
{
    public interface iShoeService : iIMSService
    {
        void SetSession(UserSession session);
        void SetConfiguration(IMSConfiguration Config);
        ShoeModel GetShoeModel(int id);
        Shoe GetShoe(int id);
        ShoeModel[] GetAllShoeModels();
        Shoe[] GetAllShoes();
        Shoe[] GetShoeWithModel(int modelId);
        ShoeModel CreateShoeModel(ShoeModel Model);
        Shoe CreateShoe(Shoe sh);
        ShoeModel UpdateShoeModel(ShoeModel model);
        Shoe UpdateShoe(Shoe shoe);
        void DeleteShoeModel(int id);
        void DeleteShoe(int id);
    }


    public class ShoeService : IMSService , iShoeService
    {
        public StoreDbContext _context;
        private IMSConfiguration _config;
        private UserSession _session;

        public ShoeService(StoreDbContext context)
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

        public ShoeModel GetShoeModel(int id)
        {
            return _context.ShoeModels.Find(id);
        }

        public Shoe GetShoe(int id)
        {
            return _context.Shoes.Find(id);
        }

        public ShoeModel[] GetAllShoeModels()
        {
            return _context.ShoeModels.ToArray();
        }

        public Shoe[] GetAllShoes()
        {
            return _context.Shoes.ToArray();
        }

        public Shoe[] GetShoeWithModel(int modelId)
        {
            return _context.Shoes.Where(m => m.ShoeModelId == modelId).ToArray();
        }

        public ShoeModel CreateShoeModel(ShoeModel model)
        {
            _context.ShoeModels.Add(model);
            _context.SaveChanges();

            //byte[] f;
            //var imageString = model.ImageThumbnail.Split(',');
            //f = Convert.FromBase64String(imageString[1]);
            //var ct = imageString[0].Split(';')[0].Split(':')[1];
            //Document doc = new Document() { fileName = model.Id + "_Shoe_Pic", File = f, extentsion = ct };
            //_context.Documents.Add(doc);
            //_context.SaveChanges();
            //model.ImageId = doc.ID;
            //model.ImageThumbnail = "";
            //_context.SaveChanges();

            return model;
        }

        public Shoe CreateShoe(Shoe sh)
        {
            _context.Shoes.Add(sh);
            _context.SaveChanges();
            return sh;
        }

        public ShoeModel UpdateShoeModel(ShoeModel model)
        {
            _context.ShoeModels.Update(model);
            _context.SaveChanges();
            return model;
        }

        public Shoe UpdateShoe(Shoe shoe)
        {
            _context.Shoes.Update(shoe);
            _context.SaveChanges();
            return shoe;
        }

        public void DeleteShoeModel(int id)
        {
            var shoe = _context.ShoeModels.Find(id);
            _context.ShoeModels.Remove(shoe);
            _context.SaveChanges();
        }

        public void DeleteShoe(int id)
        {
            var shoe = _context.Shoes.Find(id);
            _context.Shoes.Remove(shoe);
            _context.SaveChanges();
        }


    }
}
