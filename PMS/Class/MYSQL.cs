using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; // ดึง Library มาใช้งาน

namespace PMS
{
    internal class SQL
    {
        // 1. เพิ่ม Connection Timeout (ใน String เชื่อมต่อ) กำหนดเป็นวินาที เช่น 15 วินาที
        // หากเซิร์ฟเวอร์ล่มหรือติดต่อไม่ได้ภายใน 15 วินาที จะตัดการทำงานทันที
        private static readonly string MySQLDatabaseServer =
            "datasource=127.0.0.1;" +
            "port=3306;" +
            "username=PMS;" +
            "password=PMS0982614624;" +
            "Connection Timeout=15;";

        // 2. กำหนดค่า Command Timeout (หน่วยเป็นวินาที) สำหรับการรอผลลัพธ์ของคิวรี
        private static readonly int QueryTimeoutSeconds = 30; // คิวรีห้ามทำงานเกิน 30 วินาที

        public static DataTable InputMySQLDataTable(string SQLCode)
        {
            DataTable dt = new DataTable();

            // ใช้ 'using' เพื่อเคลียร์หน่วยความจำและปิด Connection อัตโนมัติแม้จะเกิด Error
            using (MySqlConnection conMySQL = new MySqlConnection(MySQLDatabaseServer))
            {
                try
                {
                    if (conMySQL.State == ConnectionState.Closed)
                    {
                        conMySQL.Open();
                    }

                    using (MySqlCommand cmd = new MySqlCommand(SQLCode, conMySQL))
                    {
                        // 3. ตั้งค่า Command Timeout ให้กับคำสั่ง SQL นี้
                        cmd.CommandTimeout = QueryTimeoutSeconds;

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    // คุณสามารถจัดการกับข้อผิดพลาดตรงนี้ได้ เช่น เช็คว่าเป็นเพราะ Timeout หรือไม่
                    if (ex.Number == 0 || ex.Message.ToLower().Contains("timeout"))
                    {
                        throw new Exception("การเชื่อมต่อฐานข้อมูลใช้เวลานานเกินไป (Timeout)", ex);
                    }
                    throw; // ปล่อย Error อื่น ๆ ออกไปให้ชั้นบนจัดการ
                }
                finally
                {
                    // เพื่อความชัวร์ ปิดการเชื่อมต่อในบล็อก finally เสมอ
                    if (conMySQL.State == ConnectionState.Open)
                    {
                        conMySQL.Close();
                    }
                }
            }

            return dt;
        }
    }
}
