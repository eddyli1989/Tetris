using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame2
{
    class BoxLogic
    {
        int[,]  array;
        int[,] boxArray;
        int[,] shiftedBox;

        
        public int xMax;
        public int yMax;

        private int preOffsetX = 0;
        private int preOffsetY = 0;

        const int BOXX = 4;
        const int BOXY = 4;
        const int BOXCOUNT = 5;

        Vector2[,] backUpBox = new Vector2[BOXCOUNT, BOXCOUNT - 1];

        List<Vector2> boxlist = new List<Vector2>();

        public void setBoxValue(int x,int y,int V,int[,] box)
        {
            for(int i=0;i<x;i++)
             for(int j=0;j<y;j++)
             {
                 box[i,j] = V;
             }
        }

        public BoxLogic(int xMax, int yMax)
        {

            this.array = new int[xMax, yMax];

            this.xMax = xMax;
            this.yMax = yMax;

            this.boxArray = new int[BOXX,BOXY];
            this.shiftedBox = new int[BOXX, BOXY];

            setBoxValue(BOXX, BOXY, -1, shiftedBox);
            setBoxValue(BOXX, BOXY, -1, boxArray);
            setBoxValue(xMax, yMax, -1, array);

            //Array.Clear(shiftedBox, -1, BOXX * BOXY);
            //Array.Clear(boxArray, -1, BOXX * BOXY);
            //Array.Clear(array, -1, xMax * yMax);
         

            //I
            this.backUpBox[0, 0] = new Vector2(0, 1);
            this.backUpBox[0, 1] = new Vector2(1, 1);
            this.backUpBox[0, 2] = new Vector2(2, 1);
            this.backUpBox[0, 3] = new Vector2(3, 1);
           
            //Z
            this.backUpBox[1, 0] = new Vector2(0, 1);
            this.backUpBox[1, 1] = new Vector2(0, 2);
            this.backUpBox[1, 2] = new Vector2(1, 2);
            this.backUpBox[1, 3] = new Vector2(1, 3);
            //T
            this.backUpBox[2, 0] = new Vector2(0, 2);
            this.backUpBox[2, 1] = new Vector2(1, 1);
            this.backUpBox[2, 2] = new Vector2(1, 2);
            this.backUpBox[2, 3] = new Vector2(1, 3);
            //L
            this.backUpBox[3, 0] = new Vector2(0, 1);
            this.backUpBox[3, 1] = new Vector2(1, 1);
            this.backUpBox[3, 2] = new Vector2(2, 1);
            this.backUpBox[3, 3] = new Vector2(2, 2);
            //田
            this.backUpBox[4, 0] = new Vector2(0, 1);
            this.backUpBox[4, 1] = new Vector2(0, 2);
            this.backUpBox[4, 2] = new Vector2(1, 1);
            this.backUpBox[4, 3] = new Vector2(1, 2);
       

        }

        public bool setData(int x, int y, int value)
        {
            bool bSuccess = false;
            if (x < xMax && y<yMax )
            {
                this.array[x, y] = value;
                bSuccess = true;
            }
            return bSuccess;
        }

        public int getData(int x, int y)
        {
            int value = -1;
            if (x < xMax && y < yMax)
            {
             value =   (int)this.array[x, y] ;
            }
            return value;
        }


        public bool checkFall(List<Vector2> position)
        {
            return canMove(position, 1, 0);
        }

        public List<Vector2> fall(List<Vector2> position)
        {
            clearnPosition(position);
            return map(preOffsetX + 1, preOffsetY);
        }

        public List<Vector2> goDown(List<Vector2> position)
        {
           
            while (checkFall(position))
            {
               position = fall(position);
            }
            
            return position;
        }
        public bool checkDis(int x)
        {
            bool bSuccess = true;
            for (int i = 0; i < this.yMax;i++ )
            {
                if (this.array[x, i] == -1)
                {
                    bSuccess = false;
                }
            }
            return bSuccess;
        }

        public bool dis(List<Vector2> position)
        {
            bool bSuccess = false;
            foreach (Vector2 v in position)
            {
                if (checkDis((int)v.X))
                {
                    for (int i = 0; i < this.yMax; i++)
                    {
                        this.array[(int)v.X, i] = -1;
                        updateArray((int)v.X);
                    }
                    bSuccess = true;
                }
            }
            return bSuccess;
        }

        public void updateArray(int x)
        {
            for(int i=(int)x-1;i>=0;i--)
                for (int j = 0; j < this.yMax; j++)
                {
                    if (this.array[i, j] != -1 && this.array[i+1,j] == -1)
                    {
                        this.array[i + 1, j] = this.array[i, j];
                        this.array[i, j] = -1;
                    }
                }
        }
        public List<Vector2> createNewBox()
        {
            //Array.Clear(boxArray, -1, BOXX * BOXY);
            setBoxValue(BOXX, BOXY, -1, boxArray);
            int rndNum;
            int index;
            Random rnd2 = new Random();

            rndNum = rnd2.Next(0,BOXCOUNT);
            index  = (int)rnd2.Next(0,6);
            for (int i = 0; i < BOXCOUNT-1; i++)
            {
                boxArray[(int)this.backUpBox[rndNum, i].X, (int)this.backUpBox[rndNum, i].Y] = index;
            }
               
            return map(0, yMax / 2);
            
        }

       

        public List<Vector2> move(List<Vector2> position,int offsety)
        {
            Vector2 v = new Vector2(0, 0);
            if (canMap(this.boxArray,0,offsety) == v && canMove(position,0,offsety) )
            {
                clearnPosition(position);
                return map(preOffsetX, preOffsetY + offsety);
            }
            return position;
        }

        public bool canMove(List<Vector2> position, int offsetX,int offsetY)
        {
            bool bSuccess = true;

            foreach (Vector2 v in position)
            {
                int dx = (int)v.X + offsetX;
                int dy = (int)v.Y + offsetY;
                
                if (dx >= this.xMax || dx < 0 || dy >= this.yMax || dy < 0)
                {
                    bSuccess = false;
                    break;
                }
                if (this.array[dx, dy] != -1)
                {
                    bool isin = false;
                    foreach (Vector2 k in position)
                    {
                        if ((int)k.X == dx && (int)k.Y == dy)
                        {
                            isin = true;
                            break;
                        }
                    }
                    if (!isin)
                    {
                        bSuccess = false;
                        break;
                    }
                }
                
            }
            
            return bSuccess;
        }
        public Vector2 canMap(int[,] boxArray,int Offsetx,int Offsety)
        {
             Vector2 bSuccess = new Vector2(0,0);
             for(int i=0;i<BOXX;i++)
                 for (int j = 0; j < BOXY; j++)
                 {
                     if (boxArray[i, j] != -1)
                     {
                         if ((j + this.preOffsetY + Offsety) < 0)
                         {
                             int pre = System.Math.Abs(j + this.preOffsetY + Offsety);
                             if (System.Math.Abs(pre) > System.Math.Abs(bSuccess.Y))
                             {
                                 bSuccess.Y = pre;
                             }

                         }

                         if ( (j + this.preOffsetY+Offsety) >= this.yMax)
                         {
                             int pre = this.yMax - (j + this.preOffsetY + Offsety) - 1;
                             if (System.Math.Abs(pre) > System.Math.Abs(bSuccess.Y))
                             {
                                 bSuccess.Y = pre;
                             }
                         }

                         if ((i + this.preOffsetX + Offsetx) < 0)
                         {
                             int pre = System.Math.Abs(i + this.preOffsetX + Offsetx);
                             if (System.Math.Abs(pre) > System.Math.Abs(bSuccess.X))
                             {
                                 bSuccess.X= pre;
                             }

                         }

                         if ((i + this.preOffsetX + Offsetx) >= this.xMax)
                         {
                             int pre = this.xMax - (i+ this.preOffsetX + Offsetx) - 1;
                             if (System.Math.Abs(pre) > System.Math.Abs(bSuccess.X))
                             {
                                 bSuccess.X = pre;
                             }
                         }
                     }
                 }
             return bSuccess;

        }
        private void clearnPosition(List<Vector2> position)
        {
            foreach (Vector2 v in position)
            {
                this.array[(int)v.X, (int)v.Y] = -1;
            }
        }
        //将boxArray以一定的偏移映射到array上,返回映射的坐标集合
        private List<Vector2>  map(int xOffset,int yOffset)
        {
        
            preOffsetX = xOffset;
            preOffsetY = yOffset;
            boxlist.Clear();
            for(int i=0;i<BOXX;i++)
                for(int j=0;j<BOXY;j++)
                {
                    if (boxArray[i, j] != -1)
                    {
                        if ((i + xOffset) < this.xMax && (i + xOffset) >= 0 && (j + yOffset)>=0 && (j + yOffset) < this.yMax)
                        {
                            this.array[i + xOffset, j + yOffset] = this.boxArray[i, j];
                            boxlist.Add(new Vector2(i + xOffset, j + yOffset));
                        }
                    }
                }
            return boxlist;
        }


        public bool canShift(int[,] box, int offsetx,int offsety,int XMAX, int YMAX, List<Vector2> postion)
        {
            bool bSuccess = true;

            for (int i = 0; i < XMAX; i++)
            {
                for (int j = 0; j < YMAX; j++)
                {
                    if (box[i, j] != 0 && (i + preOffsetX + offsetx) < this.xMax && (i + preOffsetX + offsetx) >= 0 && (j + preOffsetY + offsety) >= 0 && (j + preOffsetY + offsety) < this.yMax)
                    {
                        if (this.array[i + preOffsetX + offsetx, j + preOffsetY + offsety] != -1)
                        {
                            bool isin = false;
                            foreach (Vector2 tmp in postion)
                            {
                                if ((tmp.X == (i + preOffsetX + offsetx)) && (tmp.Y == (j + preOffsetY + offsety)))
                                {
                                    isin = true;
                                    break;
                                }
                            }
                            if (!isin)
                            {
                                bSuccess = false;
                                break;
                            }
                        }
                    }
                }
                if (!bSuccess)
                {
                    break;
                }
            }
            return bSuccess;

        }
        //将boxArray顺时针旋转90度
        public List<Vector2> shiftBox(List<Vector2> postion)
        {
            //Array.Clear(shiftedBox, -1, BOXX * BOXY);
            setBoxValue(BOXX, BOXY, -1, shiftedBox);
         
            for (int i = 0; i < BOXX; i++)
                for (int j = 0; j < BOXY; j++)
                {
                    shiftedBox[i,j] = boxArray[BOXX-1-j,i];
                }
            Vector2 v = canMap(shiftedBox,0,0);

            if (canShift(shiftedBox,(int)v.X,(int)v.Y, BOXX, BOXY, postion))
            {
                    clearnPosition(postion);
                    boxArray = (int[,])shiftedBox.Clone();
                    return map(preOffsetX+(int)v.X, preOffsetY + (int)v.Y);
            }
            else
            {
                return postion;
            }
        }

        
    }
}
