using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmetkaZaNaracka.RestoranKlasi;
using SmetkaZaNaracka.Properties;

namespace SmetkaZaNaracka.SearchFilterForm
{
    public class NameDescendingSort : SortingState
    {
        NameDescendingComparer comparer;
        public NameDescendingSort(SearchFilter form)
            : base(form)
        {
            comparer = new NameDescendingComparer();
        }

        public override void Name()
        {
            Form.SortingState = Form.NameAscendingSort;
            Form.pbIme.Image = Resources.LightArrowUp;
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
