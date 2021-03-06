﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;


namespace SGS_TEST_SYSTEM_ConsoleAPP
{
  public static  class ArdiunoConnection
    {

        public static SerialPort mySerialPort;

        public static SerialPort IntializeAudiun() {

            try {

                mySerialPort = new SerialPort("COM4");
                mySerialPort.BaudRate = 9600;
                mySerialPort.Parity = Parity.None;
                mySerialPort.StopBits = StopBits.One;
                mySerialPort.DataBits = 8;
                mySerialPort.Handshake = Handshake.None;
                mySerialPort.RtsEnable = true;
                return mySerialPort;

            }
            catch (Exception Ex) {

            }
            return mySerialPort;
        }
    }
}
