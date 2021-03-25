using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness
{
    public struct Period
    {
        private readonly int from, to;
        private bool empty;

        public Period(int from, int to)
        {
            this.empty = false;
            this.from = from;
            this.to = to;
        }
        /* Constructs an empty Period.
            This cannot be done elegantly by a constructor, because parameterless
            constructors cannot be used for structs */
        public static Period MakeEmptyPeriod()
        {
            Period res = new Period();
            res.empty = true;
            return res;
        }

        /* Assumed as a precondition that the Period is non-empty */
        public int From
        {
            get
            {
                if (this.Empty)
                    throw new PeriodAccessException();
                else return from;
            }
        }
        /* Assumed as a precondition that the Period is non-empty */
        public int To
        {
            get
            {
                if (this.Empty)
                    throw new PeriodAccessException();
                else return to;
            }
        }

        public bool Empty
        {
            get { return empty; }
        }
        /* Return the orientation of the Period.
           Returns -1 in case from is larger than to,
           1 if from is small than to, and 0 is the Period is
           empty or singular. */
        private int Orientation
        {
            get
            {
                int res;
                if (empty)
                    res = 0;
                else if (from < to)
                    res = 1;
                else if (to < from)
                    res = -1;
                else res = 0;
                return res;
            }
        }

        public int Length
        {
            get { return empty ? 0 : Math.Abs(to - from) + 1; }
        }
        /* Return element i of the Period, relative to its From edge.
           Like for arrays, the indexing is zero-bound */
        public int this[int i]
        {
            get
            {
                if (empty)
                    throw new IndexOutOfRangeException("Error");
                else if (from <= to)
                {
                    if (i >= 0 && i <= Math.Abs(from - to))
                        return from + i;
                    else throw new IndexOutOfRangeException("Error");
                }
                else if (from > to)
                {
                    if (i >= 0 && i <= Math.Abs(from - to))
                        return from - i;
                    else throw new IndexOutOfRangeException("Error");
                }
                else throw new Exception("Should not happen");
            }
        }
        /* The current Period determines the orientation of the resulting Period*/
        public Period OverlapWith(Period other)
        {
            // Enforce positively oriented Periods:
            Period thisPI = (this.Orientation < 0) ? !this : this,
                otherPI = (other.Orientation < 0) ? !other : other,
                res;

            /* In the if-else chain we work with positively oriented Periods 
             * In such Periods From <= To: */
            if (thisPI.Empty || otherPI.Empty)                                  //Both empty
                res = MakeEmptyPeriod();
            else if (thisPI.From > otherPI.To || thisPI.To < otherPI.From)      //disjoint 
                res = MakeEmptyPeriod();
            else if (thisPI.From < otherPI.From && otherPI.To < thisPI.To)      //other inside this
                res = thisPI;
            else if (otherPI.From <= thisPI.From && thisPI.To <= otherPI.To)    //this inside the other
                res = otherPI;
            else if (thisPI.From <= otherPI.From && otherPI.From <= thisPI.To)  //this overlaps left border of other
                res = new Period(otherPI.From, thisPI.To);
            else if (otherPI.From <= thisPI.From && thisPI.From <= otherPI.To)  //other overlaps left border of this 
                res = new Period(thisPI.From, otherPI.To);
            else throw new Exception("Should not happen");

            // Reintroduce orientation:
            return (this.Orientation < 0) ? !res : res;
        }

        public static Period operator +(Period i, int j)
        {
            return i.empty ? MakeEmptyPeriod() : new Period(i.From + j, i.To + j);
        }
        public static Period operator +(int j, Period i)
        {
            return i.Empty ? MakeEmptyPeriod() : new Period(i.From + j, i.To + j);
        }
        public static Period operator >>(Period i, int j)
        {
            return i.Empty ? MakeEmptyPeriod() : new Period(i.From, i.To + j);
        }
        public static Period operator <<(Period i, int j)
        {
            return i.Empty ? MakeEmptyPeriod() : new Period(i.From + j, i.To);
        }
        public static Period operator *(Period i, int j)
        {
            return i.Empty ? MakeEmptyPeriod() : new Period(i.From * j, i.To * j);
        }

        public static Period operator *(int j, Period i)
        {
            return i.Empty ? MakeEmptyPeriod() : new Period(i.From * j, i.To * j);
        }
        public static Period operator -(Period i, int j)
        {
            return i.Empty ? MakeEmptyPeriod() : new Period(i.From - j, i.To - j);
        }
        public static Period operator !(Period i)
        {
            return i.Empty ? MakeEmptyPeriod() : new Period(i.To, i.From);
        }
        public static explicit operator int[](Period i)
        {
            int[] res = new int[i.Length];
            for (int j = 0; j < i.Length; j++) res[j] = i[j];
            return res;
        }
        private class PeriodEnumerator : IEnumerator
        {
            private readonly Period Period;
            private int idx;
            public PeriodEnumerator(Period i)
            {
                this.Period = i;
                idx = -1; // position enumerator outside range
            }
            public Object Current
            {
                get
                {
                    return (Period.From < Period.To) ?
                    Period.From + idx :
                    Period.From - idx;
                }
            }
            public bool MoveNext()
            {
                if (Period.Empty)
                    return false;
                else if (idx < Math.Abs(Period.To - Period.From))
                { idx++; return true; }
                else
                { return false; }
            }
            public void Reset()
            {
                idx = -1;
            }

        }

        public IEnumerator GetEnumerator()
        {
            return new PeriodEnumerator(this);
        }
    }
}
