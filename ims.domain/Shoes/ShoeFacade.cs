using ims.data;
using ims.data.Entities;
using ims.domain.Infrastructure;
using ims.domain.Infrastructure.Architecture;
using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Shoes
{

    public  interface IShoeFacade : IiMSFacade
    {
        void SetConfiguration(IMSConfiguration Config);
        void SetSession(UserSession session);
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

    public class ShoeFacade : IMSFacade, IShoeFacade
    {

        private iShoeService _shoeService;
        private StoreDbContext _context;
        private UserSession _session;
        private IMSConfiguration _config;
        public ShoeFacade(StoreDbContext Context, iShoeService shoeService) : base(Context)
        {
            _context = Context;
            _shoeService = shoeService;
            _shoeService.SetContext(Context);
            PassContext(_shoeService, Context);
        }

        public void SetSession(UserSession session)
        {
            _session = session;
            _shoeService.SetSession(session);

        }

        public void SetConfiguration(IMSConfiguration config)
        {
            _config = config;
            _shoeService.SetConfiguration(config);
        }

        public ShoeModel GetShoeModel(int id)
        {
            return _shoeService.GetShoeModel(id);
        }

        public Shoe GetShoe(int id)
        {
            return _shoeService.GetShoe(id);
        }

        public ShoeModel[] GetAllShoeModels()
        {
            return _shoeService.GetAllShoeModels();
        }

        public Shoe[] GetAllShoes()
        {
            return _shoeService.GetAllShoes();
        }

        public Shoe[] GetShoeWithModel(int modelId)
        {
            return _shoeService.GetShoeWithModel(modelId);
        }

        public ShoeModel CreateShoeModel(ShoeModel Model)
        {
            return Transact(t =>
            {
                return _shoeService.CreateShoeModel(Model);
            });
        }

        public Shoe CreateShoe(Shoe sh)
        {
            return Transact(t =>
            {
                return _shoeService.CreateShoe(sh);
            });
        }

        public ShoeModel UpdateShoeModel(ShoeModel model)
        {
            return Transact(t =>
            {
                return _shoeService.UpdateShoeModel(model);
            });
        }

        public Shoe UpdateShoe(Shoe shoe)
        {
            return Transact(t =>
            {
                return _shoeService.UpdateShoe(shoe);
            });
        }

        public void DeleteShoeModel(int id)
        {
            Transact(t =>
            {
                _shoeService.DeleteShoeModel(id);
            });
        }

        public void DeleteShoe(int id)
        {
            Transact(t =>
            {
                _shoeService.DeleteShoe(id);
            });
        }
    }
}
