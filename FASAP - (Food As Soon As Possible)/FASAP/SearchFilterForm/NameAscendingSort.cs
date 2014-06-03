using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmetkaZaNaracka.RestoranKlasi;
using SmetkaZaNaracka.Properties;

namespace SmetkaZaNaracka.SearchFilterForm
{
    public class NameAscendingSort : SortingState
    {
        NameAscendingComparer comparer;
        public NameAscendingSort(SearchFilter form)
            : base(form)
        {
            comparer = new NameAscendingComparer();
        }
        
        public override void Name()
        {
            Form.SortingState = Form.NameDescendingSort;
            Form.pbIme.Image = Resources.LightArrowDown;
            Form.SortingState.Evaluate();
        }

        public override void Rejting()
        {
            Form.SortingState = Form.RejtingDescendingSort;
            Form.pbIme.Image = Resources.DarkArrowDown;
            Form.pbRejting.Image = Resources.LightArrowDown;
            Form.SortingState.Evaluate();
        }

        public override void Evaluate()
        {
            Form.Restorani.Sort(comparer);
            Form.PostaviRestorani();
        }
    }
}
