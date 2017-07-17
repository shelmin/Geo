//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace GeoDemo
//{
    
//        /// </summary>
//        /// <param name="wellPlotInfo"></param>
//        /// <returns></returns>
//        public bool OpenCrossInfoStream(byte[] plotInfoStream)
//        {	
//            if (plotInfoStream == null || plotInfoStream.Length == 0)
//            {
//                return false;
//            }
			
//            MemoryStream stream = null;
//            try
//            {
//                stream = new MemoryStream(plotInfoStream);
//                BinaryFormatter bformatter = new BinaryFormatter();
//                CrossPlotDoc tempDoc = new CrossPlotDoc();
//                //CrossPlotDoc tempDoc = (CrossPlotDoc)bformatter.Deserialize(stream);
//                tempDoc.Deserialize(stream, bformatter);
//                this.Dispose();
//                Init(tempDoc);
//                this.crossForm.ChangePanelSize();
//            }
//            catch (Exception e)
//            {
//                MessageBox.Show("打开地质插图信息出现错误：" + e.Message);
//                if (stream != null)
//                {
//                    stream.Close();
//                }
//                return false;
//            }
//            finally
//            {				
//                if (stream != null)
//                {
//                    stream.Close();
//                }	
//            }
//            return true;
//        }   

//private void openTemplateFromDBToolMenu_Click(object sender, EventArgs e)
//        {
//            AccessModuleSubItem tempItem;
//            if (!AccessInfo.CanGetModuleSubItem(ModuleType.CrossPlot, AccessType.OpenTemplate, out tempItem))
//            {
//                return;
//            }
//            SelectTemplateDlg dlg = new SelectTemplateDlg();
//            dlg.ShowDialog();
//            if (dlg.Succeed && dlg.ProjectTemplate != null)
//            {
//                doc.ProjectTemplate = dlg.ProjectTemplate;
//                doc.OpenCrossInfoStream(dlg.ProjectTemplate.Data);
//                SetDocSet(true);
//                SaveModuleSubItem(tempItem);
//            }
//            SetText();
//            dlg.Dispose();
//        }

//    private void openInfoFromDBToolMenu_Click(object sender, EventArgs e)
//        {
//            AccessModuleSubItem tempItem;
//            if (!AccessInfo.CanGetModuleSubItem(ModuleType.CrossPlot, AccessType.OpenInfo, out tempItem))
//            {
//                return;
//            }
//            doc.OpenSelectCrossInfoDlg();
//            this.SaveModuleSubItem(tempItem);
//            this.SetText();
//        }

//   private void SaveModuleSubItem(AccessModuleSubItem subItem)
//        {
//            if (subItem != null)
//            {
//                foreach (DrawObj drawObj in doc.DrawObjs)
//                {
//                    if (drawObj is LogD2CrossObj)
//                    {
//                        LogD2CrossObj logD2CrossObj = (drawObj as LogD2CrossObj);
//                        if (!subItem.WellNames.Contains(logD2CrossObj.Well.Name))
//                        {
//                            subItem.WellNames.Add(logD2CrossObj.Well.Name);
//                        }
//                    }
//                }

//                string infoName = "";
//                if (doc.ProjectTemplate != null)
//                {
//                    infoName = "模板[" + doc.ProjectTemplate.Name + "] ";
//                }
//                if (doc.ProjectInfo != null)
//                {
//                    infoName += "信息[" + doc.ProjectInfo.Name + "]";
//                }
//                subItem.InfoName = infoName;
//                subItem.EndTime = DateTime.Now;                
//                Ymhdo.Self.AccessInfo.Save();
//            }
//        }

//    /// <summary>
//        /// 打开选择地质插图信对画框
//        /// </summary>
//        public void OpenSelectCrossInfoDlg()
//        {
//            ArrayList projectInfoes = Ymhdo.Project.GetInfoes(ShareInfo.GeoAuxMap);
//            if (projectInfoes.Count < 1)
//            {
//                MessageBox.Show("库中没有地质插图信息!");
//                return;
//            }
//            else 
//            {
//                SelectInfoDlg dlg = new SelectInfoDlg(projectInfoes);
//                dlg.ShowDialog();
//                if (dlg.Succeed && dlg.ProjectInfo != null)
//                {
//                    projectInfo = dlg.ProjectInfo;
//                    bool succeed = this.OpenCrossInfoStream(projectInfo.Info);
//                    if (!succeed)
//                    {
//                        MessageBox.Show("本项目地质插图信息出错，将被删除!");
//                        projectInfo.Delete();
//                    }
//                    else
//                    {
//                        crossForm.SetDocSet(true);
//                    }
//                }
//                dlg.Dispose();
//            }
//        }

//    private void saveAsTemplate2DBToolMenu_Click(object sender, EventArgs e)
//        {
//            if (doc.DrawObjs.Count < 1)
//            {
//                MessageBox.Show("你还没绘制图件，不能保存模版!");
//                return;
//            }
//            ProjectTemplate tempTemplate;
//            if (Ymhdo.Project.SaveTemplateAs(ShareInfo.GeoAuxMap, out tempTemplate))
//            {
//                doc.ProjectTemplate = tempTemplate;
//                doc.CrossForm.SaveProjectTemplate2DB();
//            }
//        }


//          private void saveInfo2FileToolMenu_Click(object sender, EventArgs e)
//        { 
//            if (doc.DrawObjs.Count < 1)
//            {
//                MessageBox.Show("你还没绘制图件，不能保存信息!");
//                return;
//            }
//            SaveFileDialog dlg = new SaveFileDialog();
//            dlg.FileName = Ymhdo.Project.Name;
//            dlg.Filter = "文件(*.CrossPlotInfo)|*.CrossPlotInfo|所有文件 (*.*)|*.*";
//            if (dlg.ShowDialog() == DialogResult.OK)
//            {
//                MemoryStream memoryStream = new MemoryStream();
//                BinaryFormatter bformatter = new BinaryFormatter();
//                doc.Serialize(memoryStream, bformatter);

//                Stream stream = File.Open(dlg.FileName, FileMode.Create);
//                bformatter.Serialize(stream, memoryStream.ToArray());
//                stream.Close();
//                memoryStream.Close();
//            }
//        }

//  private void openInfoFromFileToolMenu_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog dlg = new OpenFileDialog();
//            dlg.Filter = "文件 (*.CrossPlotInfo)|*.CrossPlotInfo;|所有文件 (*.*)|*.*";
//            dlg.FilterIndex = 1;
//            if (dlg.ShowDialog() == DialogResult.OK)
//            {
//                Stream stream = null;
//                CrossPlotDoc tempDoc = new CrossPlotDoc();
//                try
//                {
//                    stream = File.Open(dlg.FileName, FileMode.Open);
//                    BinaryFormatter bformatter = new BinaryFormatter();
//                    byte[] bytes = (byte[])bformatter.Deserialize(stream);
//                    MemoryStream memoryStream = new MemoryStream(bytes);
//                    BinaryFormatter bformatter1 = new BinaryFormatter();
//                    tempDoc.Deserialize(memoryStream, bformatter1);
//                    stream.Close();
//                    memoryStream.Close();
//                }
//                catch (Exception ee)
//                {
//                    MessageBox.Show(ee.Message);
//                    return;
//                }
//                if (stream != null)
//                {
//                    if (tempDoc != null)
//                    {
//                        doc.Dispose();
//                        doc.Init(tempDoc);
//                        doc.FocusObj = null;
//                        ChangePanelSize();
//                        SetDocSet(true);
//                    }
//                    stream.Close();
//                }
//            }
//            dlg.Dispose();	
//        }




//        private void saveTemplate2DBToolMenu_Click(object sender, EventArgs e)
//        {
//            if (doc.DrawObjs.Count < 1)
//            {
//                MessageBox.Show("你还没绘制图件，不能保存模版!");
//                return;
//            }
//            if (!Ymhdo.Project.ExistTemplate(doc.ProjectTemplate))
//            {
//                MessageBox.Show("当前模版是空， 请选择 另存绘图模版到库!");
//                return;
//            }
//            doc.CrossForm.SaveProjectTemplate2DB();
//        }


//        private void SaveProjectTemplate2DB()
//        {
//            if (doc.ProjectTemplate == null)
//            {
//                MessageBox.Show("当前地质插图模板是空，你该选择另存地质插图模板到库中！");
//                return;
//            }
            
//            if (doc.ProjectTemplate.ReadOnly)
//            {
//                MessageBox.Show("只有管理员才能保存模板！");
//                return;
//            }

//            MemoryStream stream = new MemoryStream();
//            BinaryFormatter bformatter = new BinaryFormatter();
//            //bformatter.Serialize(stream, doc);
//            doc.Serialize(stream, bformatter);
//            doc.ProjectTemplate.Data = stream.ToArray();
//            stream.Close();			
//            doc.ProjectTemplate.Save();
//        }

//        private bool SaveCrossInfo2DB(ProjectInfo projectInfo)
//        {
//            if (Ymhdo.Project.ReadOnly)
//            {
//                MessageBox.Show("本项目不是你创建的，你不能修改本项目地质插图信息!");
//                return false;
//            }

//            if (doc.DrawObjs.Count < 1)
//            {
//                MessageBox.Show("你还没绘制图件，不能保存信息!");
//                return false;
//            }

//            if (projectInfo == null)
//            {
//                MessageBox.Show("当前地质插图信息是空，你该选择另存地质插图信息到库菜单！");
//                return false;
//            }	
		
//            MemoryStream stream = new MemoryStream();
//            BinaryFormatter bformatter = new BinaryFormatter();
//            //bformatter.Serialize(stream, doc);
//            doc.Serialize(stream, bformatter);
//            projectInfo.Info = stream.ToArray();
//            stream.Close();			
//            return projectInfo.Save();
//        }	

//        private bool SaveWellSiteMapInfo2DB(ProjectInfo projectInfo)
//        {
//            if (Ymhdo.Project.ReadOnly)
//            {
//                MessageBox.Show("本项目不是你创建的，你不能修改本项目地质插图信息!");
//                return false;
//            } 

//            if (projectInfo == null)
//            {
//                MessageBox.Show("当前地质插图信息是空，你该选择另存地质插图信息到库菜单！");
//                return false;
//            }	
		
//            MemoryStream stream = new MemoryStream();
//            BinaryFormatter bformatter = new BinaryFormatter();
//            //bformatter.Serialize(stream, doc);
//            doc.Serialize(stream, bformatter);
//            projectInfo.Info = stream.ToArray();
//            stream.Close();			
//            return projectInfo.Save();
//        }	

//}
