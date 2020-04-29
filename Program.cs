using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, List<int>> dict = new Dictionary<char, List<int>>();

            List<List<int>> resultData = new List<List<int>>();

            string a = "abwfwcsgafgbcwawdfa";
            string b = "abc";

            for (int i = 0; i < b.Length; i++)
            {
                if (dict.ContainsKey(b[i]))
                {
                    continue;
                }

                dict.Add(b[i], new List<int>());
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (dict.ContainsKey(a[i]))
                {
                    dict[a[i]].Add(i);
                }
            }

            resultData.Clear();

            foreach (var key in dict.Keys)
            {
                resultData.Add(dict[key]);
            }

            TreeObject<int?> root = new TreeObject<int?>(null);

            for (int i = 0; i < resultData.Count; i++)
            {
                List<TreeObject<int?>> parent = root.GetLayerObjects(i);

                if (parent.Count <= 0)
                {
                    Console.WriteLine("当前层无节点");
                    break;
                }

                for (int k = 0; k < resultData[i].Count; k++)
                {
                    for (int j = 0; j < parent.Count; j++)
                    {
                        TreeObject<int?> data = new TreeObject<int?>(resultData[i][k]);
                        parent[j].AddChildren(data);
                    }
                }
            }

            List<List<TreeObject<int?>>> allBranch = root.GetAllBranch();

            int? minLength = null;
            List<TreeObject<int?>> resultList = null;

            for (int i = 0; i < allBranch.Count; i++)
            {
                int? min = null;
                int? max = null;

                for (int k = 0; k < allBranch[i].Count; k++)
                {
                    var data = allBranch[i][k].Content;

                    if (min == null)
                    {
                        min = data;
                    }

                    if (max == null)
                    {
                        max = data;
                    }

                    if (min > data)
                    {
                        min = data;
                    }

                    if (max < data)
                    {
                        max = data;
                    }

                    if (k == allBranch[i].Count - 1)
                    {
                        Console.WriteLine($"max:{max}+min:{min}");

                        var result = max - min;

                        if (minLength == null)
                        {
                            minLength = result;
                            resultList = allBranch[i];
                        }
                        else if (minLength > result)
                        {
                            minLength = result;
                            resultList = allBranch[i];
                        }

                        Console.WriteLine(result);
                    }
                }
            }

            for (int i = 0; i < allBranch.Count; i++)
            {
                for (int j = 0; j < allBranch[i].Count; j++)
                {
                    if (j < allBranch[i].Count - 1)
                    {
                        Console.Write(allBranch[i][j].Content + ",");
                    }
                    else
                    {
                        Console.Write(allBranch[i][j].Content);
                    }
                }
                Console.Write("\n");
            }

            if (resultList != null)
            {
                for (int i = 0; i < resultList.Count; i++)
                {
                    Console.WriteLine($"result:{resultList[i].Content}");
                }
            }
            Console.ReadLine();
        }
    }

    public class TreeObject<T>
    {
        public TreeObject<T> pTreeObject;
        public T Content { set; get; }
        public List<TreeObject<T>> children = new List<TreeObject<T>>();

        public TreeObject(T content)
        {
            this.Content = content;
        }

        public virtual void AddChildren(TreeObject<T> obj)
        {
            obj.pTreeObject = this;
            children.Add(obj);
        }

        public List<TreeObject<T>> GetLayerObjects(int layer)
        {
            if (layer <= 0)
            {
                List<TreeObject<T>> data = new List<TreeObject<T>>();
                data.Add(this);
                return data;
            }

            return GetLayerMethod(layer, children);
        }

        private List<TreeObject<T>> GetLayerMethod(int layer, List<TreeObject<T>> sourceData)
        {
            if (layer > 1)
            {
                List<TreeObject<T>> resultData = new List<TreeObject<T>>();

                foreach (var child in sourceData)
                {
                    for (int i = 0; i < child.children.Count; i++)
                    {
                        resultData.Add(child.children[i]);
                    }
                }

                layer -= 1;

                return GetLayerMethod(layer, resultData);
            }
            else
            {
                return sourceData;
            }
        }

        public List<List<TreeObject<T>>> GetAllBranch()
        {
            List<List<TreeObject<T>>> branch = new List<List<TreeObject<T>>>();

            List<TreeObject<T>> result = new List<TreeObject<T>>();

            GetLastTreeObject(children, result);

            foreach (var child in result)
            {
                branch.Add(GetBranch(child));
            }

            return branch;
        }

        private List<TreeObject<T>> GetBranch(TreeObject<T> obj)
        {
            List<TreeObject<T>> branch = new List<TreeObject<T>>();

            CheckBranch(branch, obj);

            return branch;
        }

        private void CheckBranch(List<TreeObject<T>> result, TreeObject<T> obj)
        {
            if (obj.Content != null)
                result.Add(obj);

            if (obj.pTreeObject != null)
            {
                CheckBranch(result, obj.pTreeObject);
            }
        }

        private void GetLastTreeObject(List<TreeObject<T>> data, List<TreeObject<T>> result)
        {
            foreach (var child in data)
            {
                if (child.children.Count == 0)
                {
                    result.Add(child);
                }
                else
                {
                    GetLastTreeObject(child.children, result);
                }
            }
        }
    }
}