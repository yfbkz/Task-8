using System.Net;
using System.Runtime.Intrinsics.X86;

namespace Task_7
{
    public class T7
    {
        public static string res;
        string letters = "abcdefghijklmnopqrstuvwxyz";
        string incor = "";
        public static List<Object> NewString(string s, char m)
        {
            List<Object> RetList = new List<Object>();
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string incor = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (!letters.Contains(Convert.ToString(s[i])))
                {
                    incor += $"{s[i]} ";
                }
            }
            string s4 = "";
            int a = s.Length - 1;
            if (s.Length % 2 == 0)
            {
                a = a / 2;
                string s2 = s.Substring(0, a + 1);
                string s3 = s.Substring(a + 1);
                for (int i = 0; i <= a; i++)
                {
                    s4 = s4 + s2[a - i];
                }
                for (int i = 0; i <= a; i++)
                {
                    s4 = s4 + s3[a - i];
                }
            }
            else
            {
                for (int i = 0; i <= a; i++)
                {
                    s4 = s4 + s[a - i];
                }
                s4 = s4 + s;
            }
            RetList.Add(s4);
            RetList.Add(CountOfLetters(s4));
            RetList.Add(LongestString(s4));
            RetList.Add("Sorted string: "+SortString(s4, m));
            RetList.Add(DeleteLetter(s4));

            return (RetList);

        }
        public static object CountOfLetters(string s)
        {
            
            char[] chars = s.Distinct().ToArray();
            List<String> list = new List<string>();
            for (int i = 0; i < chars.Length; i++)
            {
                int q = 0;
                for (int j = 0; j < s.Length; j++)
                {
                    if (chars[i] == s[j])
                    {
                        q += 1;
                    }
                }
                 list.Add($"{chars[i]}: {q}");
            }
            return list;
        }
        public static string LongestString(string s)
        {
            res= "Substring is empty.";
            for (int i = 0; i < s.Length; i++)
            {
                if ("aeiouy".Contains(s[i]))
                {
                    for (int j = s.Length - 1; j >= 0; j--)
                    {
                        if ("aeiouy".Contains(s[j]))
                        {
                            res=$"Substring: {s.Substring(i, j - i + 1)}";
                            break;

                        }
                    }
                    break;
                }
            }
            return res;
        }
        public static string SortString(string s, char m)
        {
            res = "Incorrect method input.";
            if (m == 'T') { res=String.Join("", Sort.TreeSort(s.ToCharArray())); }
            else if (m == 'Q')
            {
                res=String.Join("", Sort.QuickSort(s.ToCharArray()));
            }
            return res;
        }

        static string DeleteLetter(string str)
        {
            int ind;
            res = "";
            try
            {
                string text = $"http://www.randomnumberapi.com/api/v1.0/random?min=0&max={str.Length - 1}&count=1";
                WebRequest wr = WebRequest.Create(text);
                Stream objSt = wr.GetResponse().GetResponseStream();
                StreamReader objRd = new StreamReader(objSt);
                ind = Convert.ToInt16(objRd.ReadLine().Trim('[', ']'));

            }
            catch (Exception ex)
            {
                res=$"({ex.Message})  ";  
                Random random = new Random();
                ind = random.Next(0, str.Length - 1);
            }

            res+=$"Trimmed string: {str.Remove(ind, 1)} (Deleted index:{ind})";
            return res;
        }


        public abstract class Sort
        {
            static void Swap(ref char x, ref char y)
            {
                var t = x;
                x = y;
                y = t;
            }

            static int Partition(char[] array, int minIndex, int maxIndex)
            {
                var pivot = minIndex - 1;
                for (var i = minIndex; i < maxIndex; i++)
                {
                    if (array[i] < array[maxIndex])
                    {
                        pivot++;
                        Swap(ref array[pivot], ref array[i]);
                    }
                }

                pivot++;
                Swap(ref array[pivot], ref array[maxIndex]);
                return pivot;
            }

            static char[] Quick(char[] array, int minIndex, int maxIndex)
            {
                if (minIndex >= maxIndex)
                {
                    return array;
                }

                var pivotIndex = Partition(array, minIndex, maxIndex);
                Quick(array, minIndex, pivotIndex - 1);
                Quick(array, pivotIndex + 1, maxIndex);

                return array;
            }

            public static char[] QuickSort(char[] array)
            {
                return Quick(array, 0, array.Length - 1);
            }




            public static char[] TreeSort(char[] array)
            {
                var treeNode = new TreeNode(array[0]);
                for (int i = 1; i < array.Length; i++)
                {
                    treeNode.Insert(new TreeNode(array[i]));
                }

                return treeNode.Transform();
            }
            public class TreeNode
            {
                public TreeNode(char data)
                {
                    Data = data;
                }

                public char Data { get; set; }
                public TreeNode Left { get; set; }
                public TreeNode Right { get; set; }

                public void Insert(TreeNode node)
                {
                    if (node.Data < Data)
                    {
                        if (Left == null)
                        {
                            Left = node;
                        }
                        else
                        {
                            Left.Insert(node);
                        }
                    }
                    else
                    {
                        if (Right == null)
                        {
                            Right = node;
                        }
                        else
                        {
                            Right.Insert(node);
                        }
                    }
                }
                public char[] Transform(List<char> elements = null)
                {
                    if (elements == null)
                    {
                        elements = new List<char>();
                    }

                    if (Left != null)
                    {
                        Left.Transform(elements);
                    }

                    elements.Add(Data);

                    if (Right != null)
                    {
                        Right.Transform(elements);
                    }

                    return elements.ToArray();
                }
            }
        }
    }
}
