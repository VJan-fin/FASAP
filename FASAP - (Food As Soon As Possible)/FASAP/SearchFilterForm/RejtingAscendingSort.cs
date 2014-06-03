using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmetkaZaNaracka.RestoranKlasi;
using SmetkaZaNaracka.Properties;

namespace SmetkaZaNaracka.SearchFilterForm
{
    public class RejtingAscendingSort : SortingState
    {
        RejtingAscendingComparer comparer;
        public RejtingAscendingSort(SearchFilter form)
            : base(form)
        {
            comparer = new RejtingAscendingComparer();
        }

        public override void Name()
        {
            Form.SortingState = Form.NameDescendingSort;
            Form.pbRejting.Image = Resources.DarkArrowDown;
            Form.pbIme.Image = Resources.LightArrowDown;
            Form.SortingState.Evaluate();
        }

        public override void Rejting()
        {
            Form.SortingState = Form.RejtingDescendingSort;
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
