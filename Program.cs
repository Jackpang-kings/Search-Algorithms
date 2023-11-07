using System;
using System.ComponentModel.Design.Serialization;
namespace SearchingAlgorithms
{
    class BinaryTreeNode {
        int? data;
        int? count;
        BinaryTreeNode? left;
        BinaryTreeNode? right;
        public int? Data {get {return data;} set{data = value;} }
        public int? Count {get {return count;} set{count = value;} }
        public BinaryTreeNode? Left {get {return left;} set{left = value;} }
        public BinaryTreeNode? Right {get {return right;} set{right=value;}}

        public BinaryTreeNode(){
            left=null;
            right=null;
            data=null;
        }


    }

    class BinaryTree {
        BinaryTreeNode root;
        public BinaryTreeNode Root {get {return root;} }

        public BinaryTree(){
            root=new BinaryTreeNode();
        }

        public bool Add2Tree(int newData){
            bool rval=true;
            bool added=false;
            //create new node to insert          
            BinaryTreeNode newNode= new BinaryTreeNode();
            newNode.Data=newData;
            newNode.Count=1;
            BinaryTreeNode point=root;
            //check if first node
            if (point.Data is null){
                point.Data=newData;
            } else {
                //find location to add to
                do {
                    if (newData < point.Data) {
                        //navigate left
                        if (point.Left is null){
                            point.Left=newNode;
                            added=true;
                        } else {
                            point=point.Left;
                        }
                        
                    } else if  (newData > point.Data) {
                        //navigate right
                        if (point.Right is null){
                            point.Right=newNode;
                            added=true;
                        } else {
                            point=point.Right;
                        }
                    } else {
                        //increment count as duplicate value.
                        point.Count++;
                        added=true;
                        
                    }
                } while(!added);
            }
            return rval;
        }
    }

    class Program
    {
        //generate a random list of integers 
        static int[] GenerateList(int size, int low, int high){
            Random rand = new Random();
            int[] retArray= new int[size];
            for (int i=0; i<size; i++){
                retArray[i]=rand.Next(low,high);
            }
            return retArray;
        }
        
        static BinaryTree BldTree(int[] dta){
            BinaryTree tree= new BinaryTree();
            foreach (int i in dta){
                tree.Add2Tree(i);
            }
            return tree;

        }

        //copy inbound array to a new array and sort it
        static int[] SortArr(int[] dtaArr){
            int[] retArr= new int[dtaArr.Length];  
            Array.Copy(dtaArr,retArr,dtaArr.Length);
            Array.Sort(retArr);
            return retArr;
        }

        
        static void Main(string[] args)
        {
            //Create a list of random integers
            int[] items=GenerateList(10,1,20);

            //output onto the screen
            foreach (int item in items){
                Console.Write($"{item}, ");
            }
            Console.WriteLine();

            //Create a Binary Search tree of items
            BinaryTree tree=BldTree(items);

            //Build a sorted array of items
            int[] sortItems=SortArr(items);

            //Build a duplicate of items for unsorted search
            int[] unSortItems=new int[items.Length];
            Array.Copy(items, unSortItems, items.Length);

            //Add your code here
            string ch;
            bool resume = true;
            int num;
            bool result;
            while(resume == true){
                Console.WriteLine("Searching Algorithms");
                num = CheckInput();
                Console.WriteLine("1)LinearSearch 2)BinarySearch 3)BinaryTreeSearch 4)Quit");
                ch = Console.ReadLine()!;
                if (ch == "1"){
                    result = LinearSearch(num,unSortItems);
                }else if (ch == "2"){
                    result = BinarySearch(num,sortItems);
                }else if (ch == "3"){
                    result = BinaryTreeSearch(num,tree);
                }else{
                    result = false;
                    resume = false;
                }
                if (result != true){
                    Console.WriteLine("item not found");
                }else{
                    Console.WriteLine("item found");
                }
            }
            }
            static bool LinearSearch(int n,int[] ints){
                bool found = false;
                foreach (int i in ints){
                    if (n == i){
                        found = true;
                    }
                }
                return found;
            }
            static bool BinarySearch(int n,int[] ints){
                int low = 0;
                int upp = ints.Length;
                bool found = false;
                int mid;
                double m;
                while (found == false && low <= upp){
                    m = (low + upp)/2;
                    mid = Convert.ToInt32(Math.Round(m,0));
                    if (ints[mid] == n){
                        found = true;
                    }else if (ints[mid] < n){
                        low = mid + 1;
                    }else{
                        upp = mid - 1;
                    }
                }
                return found;
            }
            static bool BinaryTreeSearch(int n, BinaryTree tree){
                BinaryTreeNode currentNode = tree.Root;
                bool found = false;
                while (found == false && currentNode != null){
                    if (currentNode.Data == n){
                        found = true;
                    }else if (currentNode.Data < n){
                        currentNode = currentNode.Right;
                    }else if (currentNode.Data > n){
                        currentNode = currentNode.Left;
                    }
                }
                return found;
            }
            static int CheckInput(){
                bool success = false;
                int input = 0;
                while (success == false){
                    Console.WriteLine("Enter the number you want to search for");
                    success = int.TryParse(Console.ReadLine(),out input);
                    if (success != true){
                        Console.WriteLine("The input is not a integer");
                    }
                }
                return input;
            }
        }
    }
