﻿using System.Collections.ObjectModel;
using FastCost.DAL;

namespace FastCost.Models
{
    public class AllCosts
    {
        public ObservableCollection<Cost> Costs { get; set; } = new();

        public AllCosts()
        {

        }

        public async Task LoadCosts()
        {
            Costs.Clear();

            var costs = await App.CostRepository.GetCostsAsync();

            // Get the folder where the costs are stored.
            // string appDataPath = FileSystem.AppDataDirectory;

            // // Use Linq extensions to load the *.costs.txt files.
            // IEnumerable<Cost> costs = Directory
            //     // Select the file names from the directory
            //     .EnumerateFiles(appDataPath, "*.costs.txt")
            //     // Each file name is used to create a new Cost
            //     .Select(fileName => new Cost()
            //     {
            //         FileName = fileName,
            //         Description = File.ReadAllText(fileName),
            //         Date = File.GetCreationTime(fileName)
            //     })
            //     // With the final collection of costs, order them by date
            //     .OrderBy(cost => cost.Date);

            // Add each cost into the ObservableCollection
            foreach (Cost cost in costs.OrderBy(cost => cost.Date))
                Costs.Add(cost);
        }
    }
}
