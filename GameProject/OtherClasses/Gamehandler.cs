using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// By André Normann
/// C# 2 Game Project
/// 2019-06-04
/// </summary>
/// 
namespace GameProject
{
    public class Gamehandler
    {

        //Save values such as name, hp, atk, items etc here and then serialize
        //get information from here when you load or save

        public static void TextFileSerialize<T>(string fileName, List<T> list)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(fileName);
                for (int i = 0; i < list.Count; i++)
                {
                    writer.WriteLine(list[i].ToString());
                }
                writer.Flush();
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Opens a streamreader and reads each line inside the textdocument and
        /// adds each line into a list that gets returned.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> ReadFromTextFile(string fileName)
        {
            StreamReader reader = null;
            List<string> rowData = new List<string>();
            try
            {
                reader = new StreamReader(fileName, Encoding.UTF8);

                while (!reader.EndOfStream) //read to end of file
                {

                    //2. get the values from a row in the file in the same ordning there were written
                    rowData.Add(reader.ReadLine()); //read the whole row

                }

            }
            finally   //always performed even when no exception is thrown
            {
                //3.  Close the file
                reader.Close();
            }
            return rowData;
        }//ReadFrom..



        /// <summary>
        /// Writes the list into an xml document with the help of a filestream and xmlserializr which defines the type of 
        /// item stored in the xmldocument
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static void WriteToXMLFile<T>(string fileName, T list)
        {
            FileStream writer = new FileStream(fileName, FileMode.Create);
            XmlSerializer XML = new XmlSerializer(typeof(T));

            try
            {
                XML.Serialize(writer, list);

            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// deserializes the filestream and returns the items inside the xmldocument 
        /// to the list
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>

        public static T ReadXMLFile<T>(string fileName)
        {

            FileStream stream = new FileStream(fileName, FileMode.Open);

            try
            {
                XmlSerializer XML = new XmlSerializer(typeof(T));

                return (T)XML.Deserialize(stream);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }


        /// <summary>
        /// this method handles all damage dealt to the enemy, it takes in all the classes and enemies that are played and change the values inside the classes
        /// and then outs a string.
        /// </summary>
        /// <param name="classes"></param>
        /// <param name="enemies"></param>
        /// <param name="enemyName"></param>
        /// <param name="poison"></param>
        /// <param name="message"></param>

        public static void DoDamage(Classes classes, Enemy enemies, string enemyName, int poison, out string message)
        {
            message = "";
            int damage = 0;


            int atk = ((Classes)classes).GetChangedDamage() + poison;
            int enemyHP = ((Enemy)enemies).GetHealth() ;

            damage = enemyHP - atk;

            if (poison <= 0)
            message = $"You hit a {enemyName} for {atk} damage";
            else
                message = $"You hit a {enemyName} for {atk} damage \n+ {poison} damage as poison";
            ((Enemy)enemies).SetHealth(damage);
        }

        /// <summary>
        /// This method handles all damage taken for the character, has all the conditions that it has to abide to and calculates damage done and 
        /// then saves the values inside the classes.cs
        /// </summary>
        /// <param name="classes"></param>
        /// <param name="enemies"></param>
        /// <param name="enemyName"></param>
        /// <param name="defence"></param>
        /// <param name="message"></param>
        public static void TakeDamage(Classes classes, Enemy enemies, string enemyName, int defence,  out string message)
        {
            message = "";
            int damage = 0;
            int enemyAtk = ((Enemy)enemies).GetDamage();
            int HP = ((Classes)classes).GetChangedHealth();


            if (enemyAtk <= defence) // prevents healing
            {
                enemyAtk = 0;
            }




            damage = HP - enemyAtk;



            if (defence > 0)
            {
                if (defence < enemyAtk)
                {
                    message = $"A {enemyName} hit you for {enemyAtk} damage.\n You blocked ({defence}) damage";
                }
                else
                {
                    message = $"A {enemyName} hit you for 0 damage.\n You blocked ({defence}) damage";
                }
            }
            else
            {
                message = $"A {enemyName} hit you for {enemyAtk} damage";
            }

            ((Classes)classes).SetHealth(damage);
        }


    }


}
