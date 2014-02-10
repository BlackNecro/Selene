using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using UGUI = UnityEngine.GUI;

namespace SeleneKSP.GUI
{
    class TreeFileBrowser
    {


        private class FileInfo
        {
            public string Name;
            public string Path;
        }
        private class FolderInfo
        {
            public string Name;
            public string Path;
            public List<FileInfo> Files = new List<FileInfo>();
            public List<FolderInfo> Folders = new List<FolderInfo>();
        }

        Rect windowPos;
        Func<bool, string, bool> callback;
        string directory;
        bool draw = true;
        TreeViewHelper treeView = new TreeViewHelper();
        FolderInfo toDisplay;
        FileInfo selectedFile;
        int id;
        Vector2 scrollPosition = new Vector2();

        GUIStyle selectedStyle = new GUIStyle(UGUI.skin.button);



        public TreeFileBrowser(Func<bool, string, bool> toCall, string startDir, int windowID)
        {            
            windowPos = new Rect((float)(Screen.width / 4 * 1.5), (float)Screen.height / 4, (float)Screen.width / 4, (float)Screen.height / 2);
            directory = startDir;
            toDisplay = RebuildFolderTree(directory);
            callback = toCall;
            selectedStyle.normal = selectedStyle.active;
            id = windowID;
        }

        public void Draw()
        {
            if (draw)
            {
                GUILayout.Window(id, windowPos, DrawWindow, "Choose File");
            }
        }

        private void DrawWindow(int id)
        {
            GUILayout.BeginVertical();
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            treeView.Start();

            DrawFolder(toDisplay);


            GUILayout.EndScrollView();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("OK") && selectedFile != null)
            {
                if (callback(true, selectedFile.Path.Replace('\\','/')))
                {
                    draw = false;
                }
            }
            if (GUILayout.Button("Cancel"))
            {

                string path = "";
                if(selectedFile != null)
                {
                    path = selectedFile.Path;
                }
                if (callback(false, path))
                {
                    draw = false;
                }                                
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private void DrawFolder(FolderInfo curFolder)
        {            
            foreach (var folder in curFolder.Folders)
            {
                treeView.CollapsibleButton(folder.Name, folder);
                if (treeView.Expanded(folder))
                {
                    treeView.Indent();
                    DrawFolder(folder);
                    treeView.Unindent();
                }
            }

            foreach (var file in curFolder.Files)
            {

                GUIStyle toUse = UGUI.skin.button;
                if (selectedFile == file)
                {
                    toUse = selectedStyle;
                }
                if (treeView.SpacedButton(file.Name, toUse))
                {
                    selectedFile = file;
                }
            }
        }

        FolderInfo RebuildFolderTree(string path, string curPath = "")
        {            
            FolderInfo toReturn = new FolderInfo();
            toReturn.Path = path;
            toReturn.Name = Path.GetFileName(path);            
            foreach (var file in Directory.GetFiles(path))
            {
                
                FileInfo fileInf = new FileInfo();
                fileInf.Name = Path.GetFileName(file);            
                fileInf.Path = Path.Combine(curPath, fileInf.Name);
                toReturn.Files.Add(fileInf);
            }
            var folders = Directory.GetDirectories(path);
            foreach (var folder in folders)
            {
                toReturn.Folders.Add(RebuildFolderTree(folder, Path.Combine(curPath, Path.GetFileName(folder))));
            }
            return toReturn;
        }
    }
}
