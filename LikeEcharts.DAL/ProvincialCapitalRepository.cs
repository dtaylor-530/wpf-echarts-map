using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LikeEcharts.DAL
{
    public  class ProvincialCapitalRepo
    {

        private List<dynamic> records;

        public ProvincialCapitalRepo()
        { 
            using (var reader = new StreamReader("Capitals.csv"))
            using (var csv = new CsvReader(reader))
            {
                records = csv.GetRecords<dynamic>().ToList();
            }
        }


        public Model.XY GetProvincialCapitalPoint(string capital)
        {
            var record = records.SingleOrDefault(_ => _.Capital == capital);
         return new Model.XY
            {
                X =double.Parse(record.X),
               Y = double.Parse(record.Y)};
            }
    }
}
