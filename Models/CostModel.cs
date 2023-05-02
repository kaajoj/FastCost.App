﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using FastCost.DAL.Entities;

namespace FastCost.Models
{
    public class CostModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public decimal? Value { get; set; } = null;

        public string Description { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string FormattedDate => Date.ToString("dd.MM");

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
