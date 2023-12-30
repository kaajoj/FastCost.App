using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FastCost.DAL.Entities;

namespace FastCost.Models
{
    public class AllCosts : INotifyPropertyChanged
    {
        public ObservableCollection<CostModel> Costs { get; set; } = new();
        //public ObservableCollection<IGrouping<Category, CostModel>> GroupCosts { get; set; } = new();
        public IEnumerable<IGrouping<Category, CostModel>> GroupCosts { get; set; }

        // public DateTime selectedDate { get; set; } = DateTime.Now;
        public DateTime selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    OnPropertyChanged();
                }
            }
        }


        //[ObservableProperty]
         public decimal Sum { get; set; }
        //private decimal sum;
        //public decimal Sum
        //{
        //    get { return sum; }
        //    set
        //    {
        //        if (sum != value)
        //        {
        //            sum = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
