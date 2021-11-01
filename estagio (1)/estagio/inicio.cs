using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace estagio
{
    public partial class inicio : Form
    {
        public inicio()
        {
            InitializeComponent();
            txt_id.Enabled = false;
            txt_idade.Enabled = false;
            txt_nome.Enabled = false;
            txt_pesquisa.Enabled = false;
        }
        SqlConnection sqlcon = null;
        private string strCon = @"Data Source=ESRVACADVDCTX40\SQLEXPRESS;Initial Catalog=estagio;Integrated Security=True";
        private string strSql = string.Empty;

        private void btn_add_Click(object sender, EventArgs e)
        {
            txt_id.Enabled = true;
            txt_idade.Enabled = true;
            txt_nome.Enabled = true;
            txt_pesquisa.Enabled = true;
            
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            strSql = "insert into cliente(idclient, nome, idade) values(@idclient, @nome, @idade)";

            sqlcon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlcon);

            comando.Parameters.Add("@idclient",SqlDbType.VarChar).Value = txt_id.Text;
            comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = txt_nome.Text;
            comando.Parameters.Add("@idade", SqlDbType.VarChar).Value = txt_idade.Text;

            try
            {
                sqlcon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro realizado com sucesso!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
            txt_id.Clear();
            txt_nome.Clear();
            txt_idade.Clear();

        }

        private void inicio_Load(object sender, EventArgs e)
        {

        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            strSql = "select * from cliente where nome=@pesquisa";

            sqlcon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlcon);

            comando.Parameters.Add("@pesquisa", SqlDbType.VarChar).Value = txt_pesquisa.Text;

            try
            {
                if(txt_pesquisa.Text == string.Empty)
                {
                    MessageBox.Show("Digite um nome para buscar");
                }
                sqlcon.Open();
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.HasRows== false)
                {
                    throw new Exception("Nome não cadastrado");
                }
                dr.Read();

                txt_id.Text = Convert.ToString(dr["idclient"]);
                txt_nome.Text = Convert.ToString(dr["nome"]);
                txt_idade.Text = Convert.ToString(dr["idade"]);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
            txt_pesquisa.Clear();

        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            strSql = "update cliente set idclient=@idclient, nome=@nome, idade=@idade";

            sqlcon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlcon);

            comando.Parameters.Add("@idclient", SqlDbType.VarChar).Value = txt_id.Text;
            comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = txt_nome.Text;
            comando.Parameters.Add("@idade", SqlDbType.VarChar).Value = txt_idade.Text;

            try
            {
                sqlcon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro alterado com sucesso");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
            txt_id.Clear();
            txt_nome.Clear();
            txt_idade.Clear();
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            strSql = "delete from cliente where idclient=@idclient";

            sqlcon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlcon);

            comando.Parameters.Add("@idclient", SqlDbType.VarChar).Value = txt_id.Text;

            try
            {
                sqlcon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro excluido com sucesso");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
            txt_id.Clear();
            txt_nome.Clear();
            txt_idade.Clear();
        }
    }
}
