using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;

namespace FHSSystemRenameClient
{
    class ObservableCollectionBuilder
    {
        /// <summary>
        /// Parser to create a list object
        /// </summary>
        /// <param name="FileName">The File to parse</param>
        /// <returns>The list object which now contains the contents of the file.</returns>
        /// <exception cref="ArguementException"/>
        public static ObservableCollection<DataHolder> Parse(string FileName)
        {
            ObservableCollection<DataHolder> list = new ObservableCollection<DataHolder>();
            using (StreamReader sr = File.OpenText(FileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(',');
                    if (splitLine.GetUpperBound(0) != 1)
                        throw new System.ArgumentException("File improperly formated, check file." + Environment.NewLine +
                            "Formate should look like: 74.125.225.110,Google");
                    list.Add(new DataHolder(splitLine[0], splitLine[1], true));
                }
            }
            return list;
        }
    }
}
