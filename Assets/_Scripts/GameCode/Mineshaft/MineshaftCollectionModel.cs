using System.Collections.Generic;

namespace GameCode.Mineshaft
{
    public class MineshaftCollectionModel
    {
        private readonly Dictionary<int, MineshaftView> _views;
        private readonly Dictionary<int, MineshaftModel> _models;

        public MineshaftCollectionModel()
        {
            _views = new Dictionary<int, MineshaftView>();
            _models = new Dictionary<int, MineshaftModel>();
        }

        public void RegisterMineshaft(int mineshaftNumber, MineshaftModel model, MineshaftView view)
        {
            _views.Add(mineshaftNumber, view);
            _models.Add(mineshaftNumber, model);
        }

        public int GetCount()
        {
            return _models.Count;
        }

        public MineshaftModel GetModel(int mineshaftNumber)
        {
            return _models[mineshaftNumber];
        }

        public MineshaftView GetView(int mineshaftNumber)
        {
            return _views[mineshaftNumber];
        }
}
}