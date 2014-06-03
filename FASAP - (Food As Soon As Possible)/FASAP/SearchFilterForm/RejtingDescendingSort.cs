using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmetkaZaNaracka.Properties;
using SmetkaZaNaracka.RestoranKlasi;

namespace SmetkaZaNaracka.SearchFilterForm
{
    public class RejtingDescendingSort : SortingState
    {
        RejtingDescendingComparer comparer;
        public RejtingDescendingSort(SearchFilter form)
            : base(form)
        {
            comparer = new RejtingDescendingComparer();
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
            Form.SortingState = Form.RejtingAscendingSort;
            Form.pbRejting.Image = Resources.LightArrowUp;
            Form.SortingState.Evaluate();
        }

        public override void Evaluate()
        {
            Form.Restorani.Sort(comparer);
            Form.PostaviRestorani();
        }
    }
}
