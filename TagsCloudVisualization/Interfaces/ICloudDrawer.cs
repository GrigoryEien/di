﻿using System.Collections.Generic;
 using System.Drawing;
 
 namespace TagsCloudVisualization.Interfaces
 {
     public interface ICloudDrawer
     {
         Bitmap DrawMap(IEnumerable<WordInRect> words, DrawingConfig drawingConfig);
     }
 }