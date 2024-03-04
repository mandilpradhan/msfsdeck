namespace Loupedeck.MsfsPlugin.input.radio
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.msfs;
    using Loupedeck.MsfsPlugin.tools;

    public class RadioDisplay : DefaultInput
    {
        private bool isCom1active = true;

        public RadioDisplay()
        {
            bindings.Add(Register(BindingKeys.COM1_ACTIVE_FREQUENCY));
            bindings.Add(Register(BindingKeys.COM2_ACTIVE_FREQUENCY));
            bindings.Add(Register(BindingKeys.COM1_STBY));
            bindings.Add(Register(BindingKeys.COM2_STBY));
            bindings.Add(Register(BindingKeys.COM1_AVAILABLE));
            bindings.Add(Register(BindingKeys.COM2_AVAILABLE));
            bindings.Add(Register(BindingKeys.COM1_RADIO_SWAP));
            bindings.Add(Register(BindingKeys.COM2_RADIO_SWAP));

            AddParameter(" ", "COM1 Active Int Frequency", "Radio");
            AddParameter("  ", "COM1 Active Float Frequency", "Radio");
            AddParameter("   ", "COM1 Standby Int Frequency", "Radio");
            AddParameter("    ", "COM1 Standby Float Frequency", "Radio");
            AddParameter("     ", "COM2 Active Int Frequency", "Radio");
            AddParameter("      ", "COM2 Active Float Frequency", "Radio");
            AddParameter("       ", "COM2 Standby Int Frequency", "Radio");
            AddParameter("        ", "COM2 Standby Float Frequency", "Radio");
        }

        protected override BitmapImage GetCommandImage(string actionParameter, PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                switch (actionParameter)
                {
                    case "COM1":
                        //bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                        //bitmapBuilder.DrawText(isCom1active ? "=> COM1" : "COM1");
                        break;
                    case "COM2":
                        //bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                        //bitmapBuilder.DrawText(!isCom1active ? "=> COM2" : "COM2");
                        break;
                    case " ": // replaced COM1 Active Int with one empty space
                        //bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                        bitmapBuilder.DrawText((bindings[0].ControllerValue == 0 ? "0" : bindings[0].ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Yellow, 40);
                        break;
                    case "  ": // replaced COM1 Active Float with two empty spaces
                        //bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                        bitmapBuilder.DrawText(bindings[0].ControllerValue == 0 ? "0" : bindings[0].ControllerValue.ToString().Substring(3, 3), ImageTool.Yellow, 40);
                        break;
                    case "   ": // replaced COM1 Standby Int with three empty spaces
                        //bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                        bitmapBuilder.DrawText((bindings[2].ControllerValue == 0 ? "0" : bindings[2].ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Grey, 40);
                        break;
                    case "    ": // replaced COM1 Standby Float with four empty spaces
                        //bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[4].MsfsValue));
                        bitmapBuilder.DrawText(bindings[2].ControllerValue == 0 ? "0" : bindings[2].ControllerValue.ToString().Substring(3, 3), ImageTool.Grey, 40);
                        break;
                    case "     ": // replaced COM2 Active Int with five empty spaces
                        //bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                        bitmapBuilder.DrawText((bindings[1].ControllerValue == 0 ? "0" : bindings[1].ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Yellow, 40);
                        break;
                    case "      ": // replaced COM2 Active Float with six empty spaces
                        //bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                        bitmapBuilder.DrawText(bindings[1].ControllerValue == 0 ? "0" : bindings[1].ControllerValue.ToString().Substring(3, 3), ImageTool.Yellow, 40);
                        break;
                    case "       ": // replaced COM2 Standby Int with seven empty spaces
                        //bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                        bitmapBuilder.DrawText((bindings[3].ControllerValue == 0 ? "0" : bindings[3].ControllerValue.ToString().Substring(0, 3)) + ".", ImageTool.Grey, 40);
                        break;
                    case "        ": // replaced COM2 Standby Float with eight empty spaces
                        //bitmapBuilder.SetBackgroundImage(ImageTool.GetAvailableDisableImage(bindings[5].MsfsValue));
                        bitmapBuilder.DrawText(bindings[3].ControllerValue == 0 ? "0" : bindings[3].ControllerValue.ToString().Substring(3, 3), ImageTool.Grey, 40);
                        break;
                    case "All":
                        break;
                }
                bitmapBuilder.DrawText(actionParameter);
                return bitmapBuilder.ToImage();
            }
        }

        protected override void RunCommand(string actionParameter)
        {
            SimConnectDAO.Instance.setPlugin(Plugin);
            switch (actionParameter)
            {
                case "COM1":
                    isCom1active = true;
                    break;
                case "COM2":
                    isCom1active = false;
                    break;
                case "COM1 Active Int":
                case "COM1 Active Float":
                case "COM1 Standby Int":
                case "COM1 Standby Float":
                    bindings[6].SetControllerValue(1);
                    break;
                case "COM2 Active Int":
                case "COM2 Active Float":
                case "COM2 Standby Int":
                case "COM2 Standby Float":
                    bindings[7].SetControllerValue(1);
                    break;
                case "Int Reset":
                case "Float Reset":
                    if (isCom1active)
                        bindings[6].SetControllerValue(1);
                    else
                        bindings[7].SetControllerValue(1);
                    break;
            }
        }
    }
}
