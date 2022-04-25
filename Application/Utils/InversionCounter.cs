namespace Application.Utils
{
    public class InversionCounter
    {
        public int Inversions { get; private set; }
    
        public int GetInversions(params int[] L)
        {
            Inversions = 0;
            GetSortAndCount(L);
            return Inversions;
        }
        private int[] GetSortAndCount(params int[] L)
        {
            if(L.Length == 1)
                return L;
            
            int lenghtA = L.Length/2;
            int lenghtB = L.Length - lenghtA;

            if(L.Length %2 != 0){
                lenghtA = L.Length/2 +1;
                lenghtB = L.Length/2 ;
            }

            var arraySegment = new ArraySegment<int>(L);
            var array1 = arraySegment.Slice(0,lenghtA).ToArray();
            var array2 = arraySegment.Slice(lenghtA).ToArray();

            array1 = GetSortAndCount(array1);
            array2 = GetSortAndCount(array2);

            return MergeAndCount(array1,array2);
        }

        public  int[] MergeAndCount(int[] array1, int[] array2)
        {
            int indexA =0; 
            int indexB = 0; 
            int inversions = array1.Length;
            int totalInversions = 0;
            List<int> aux = new List<int>();

            while (true)
            {

                int num1 = array1[indexA];
                int num2 = array2[indexB];

                if(num2 < num1){
                    totalInversions += inversions;
                    indexB++;
                    aux.Add(num2);        
                }
                else{
                    indexA ++;
                    inversions --;
                    aux.Add(num1);
                }

                //Condicoes de parada
                if(indexA == array1.Length)
                {
                    for(int i =indexB ; i < array2.Length; i ++)
                    {
                        //Coloco o restante do array2 no aux
                        aux.Add(array2[i]);
                    }
                    this.Inversions += totalInversions;
                    return aux.ToArray();
                }
                if(indexB == array2.Length)
                {
                    for(int i =indexA ; i < array1.Length; i ++)
                    {
                        //Coloco o restante do array1 no aux
                        aux.Add(array1[i]);
                    }
                    this.Inversions += totalInversions;
                    return aux.ToArray();
                }
            }           
        }
    }
}