using System;
using System.Data.OleDb;
using System.Windows.Forms;


namespace PPE2_1
{
    public partial class M2L_JPO : Form
    {
        public M2L_JPO()
        {
            InitializeComponent();
        }
        private void M2L_JPO_Load(object sender, EventArgs e)
        {
            btnModifierMembre.Enabled = false;
            btnAjouterMembre.Enabled = false;
            btnSupprimerMembre.Enabled = false;
            //Déclaration de l'objet connexion
            OleDbConnection laConnexion = new OleDbConnection();
            String connexString = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=..\\..\\..\\M2L.accdb";
            laConnexion.ConnectionString = connexString;

            OleDbCommand listeLigue = new OleDbCommand(); //Création d'une nouvelle commande
            listeLigue.CommandText = "select nom from ligue,inscription where ligue.codeligue = inscription.codeligue"; //On effectue la requête SQL nécéssaire dans notre commande
            listeLigue.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader unDataReader = listeLigue.ExecuteReader(); //Lecture des données

            //Remplissage de la ComboBox "cbxLigue" avec les éléments de la BDD via le data reader
            while (unDataReader.Read())
            {
                cbxLigue.Items.Add(unDataReader.GetString(0));
            }
            laConnexion.Close();

            OleDbCommand listeCreneau = new OleDbCommand();
            listeCreneau.CommandText = "select libelléCreneau from creneau";
            listeCreneau.Connection = laConnexion;
            laConnexion.Open();
            OleDbDataReader creneauDR = listeCreneau.ExecuteReader();

            while (creneauDR.Read())
            {
                cbxCréneau.Items.Add(creneauDR.GetString(0));
            }
            laConnexion.Close();
        }

        private void cbxLigue_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Déclaration de l'objet connexion
            OleDbConnection laConnexion = new OleDbConnection();
            String connexString = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=..\\..\\..\\M2L.accdb";
            laConnexion.ConnectionString = connexString;

            OleDbCommand listeMembres = new OleDbCommand(); //Création d'une nouvelle commande
            listeMembres.CommandText = "select membres.nom from membres,ligue where ligue.codeligue = membres.codeligue and ligue.nom = '" + cbxLigue.SelectedItem + "';"; //On effectue la requête SQL nécéssaire dans notre commande
            listeMembres.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader lbxMembreDataReader = listeMembres.ExecuteReader(); //Lecture des données

            lbxMembres.Items.Clear();
            while (lbxMembreDataReader.Read())
            {
                lbxMembres.Items.Add(lbxMembreDataReader.GetString(0));
            }

            lbxMembres.Refresh();
            // Récupération du codeLigue dans une textBox cachée (nécéssaire pour le bouton MODIFIER et NOUVEAU). //
            OleDbConnection autreConnexion = new OleDbConnection();
            autreConnexion.ConnectionString = connexString;

            OleDbCommand leCodeLigue = new OleDbCommand(); //Création d'une nouvelle commande
            leCodeLigue.CommandText = "select ligue.codeligue from ligue where ligue.nom = '" + cbxLigue.SelectedItem + "';"; //On effectue la requête SQL nécéssaire dans notre commande
            leCodeLigue.Connection = laConnexion; //Jointure
            autreConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader codeLigueDR = leCodeLigue.ExecuteReader(); //Lecture des données

            while (codeLigueDR.Read())
            {
                tbxCodeLigue.Text = (codeLigueDR.GetString(0));
            }
            autreConnexion.Close();

        }

        private void lbxMembres_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnModifierMembre.Enabled = true;
            btnAjouterMembre.Enabled = true;
            btnSupprimerMembre.Enabled = true;
            //Déclaration de l'objet connexion
            OleDbConnection laConnexion = new OleDbConnection();
            String connexString = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=..\\..\\..\\M2L.accdb";
            laConnexion.ConnectionString = connexString;

            //Récupération des noms depuis tbxNomMembre
            tbxNomMembre.Text = Convert.ToString(lbxMembres.SelectedItem);

            //Récupération des prénoms depuis la BDD
            OleDbCommand prenomMembres = new OleDbCommand(); //Création d'une nouvelle commande
            prenomMembres.CommandText = "select prenom from membres where nom = '" + lbxMembres.SelectedItem + "';"; //On effectue la requête SQL nécéssaire dans notre commande
            prenomMembres.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader prenomMembresDataReader = prenomMembres.ExecuteReader(); //Lecture des données

            //Ecriture dans tbxPrénom
            while (prenomMembresDataReader.Read())
            {
                tbxPrénom.Text = (prenomMembresDataReader.GetString(0));
            }
            laConnexion.Close();

            //Récupération des n° de téléphone depuis la BDD
            OleDbCommand telMembres = new OleDbCommand(); //Création d'une nouvelle commande
            telMembres.CommandText = "select telephone from membres where nom = '" + lbxMembres.SelectedItem + "';"; //On effectue la requête SQL nécéssaire dans notre commande
            telMembres.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader telMembresDataReader = telMembres.ExecuteReader(); //Lecture des données

            //Ecriture dans tbxTelephone
            while (telMembresDataReader.Read())
            {
                tbxTéléphone.Text = (telMembresDataReader.GetString(0));
            }
            laConnexion.Close();

            //Récupération des mails depuis la BDD
            OleDbCommand mailMembres = new OleDbCommand(); //Création d'une nouvelle commande
            mailMembres.CommandText = "select mail from membres where nom = '" + lbxMembres.SelectedItem + "';"; //On effectue la requête SQL nécéssaire dans notre commande
            mailMembres.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader mailMembresDataReader = mailMembres.ExecuteReader(); //Lecture des données

            while (mailMembresDataReader.Read())
            {
                tbxMail.Text = (mailMembresDataReader.GetString(0));
            }
            laConnexion.Close();

            //Récupération du code membre depuis la BDD
            OleDbCommand codeMembre = new OleDbCommand(); //Création d'une nouvelle commande
            codeMembre.CommandText = "select codeMembre from membres where nom = '" + lbxMembres.SelectedItem + "';"; //On effectue la requête SQL nécéssaire dans notre commande
            codeMembre.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader codeMembreDR = codeMembre.ExecuteReader(); //Lecture des données

            //Ecriture dans tbxCodeMembre
            while (codeMembreDR.Read())
            {
                tbxCodeMembre.Text = (codeMembreDR.GetString(0));
            }
            laConnexion.Close();
        }

        private void btnModifierMembre_Click(object sender, EventArgs e)
        {
            //Activation / désactivation des boutons nécéssaires
            tbxNomMembre.Enabled = true;
            tbxPrénom.Enabled = true;
            tbxTéléphone.Enabled = true;
            tbxMail.Enabled = true;
            tbxCodeMembre.Enabled = false; //Nécéssaire, car une erreur apparait si on tente de modifier directement la clé primaire pour cause de doublons. La clé primaire est tout de même modifiée, mais l'erreur est présente (problème non résolu...)
            btnAjouterMembre.Enabled = false;
            btnSupprimerMembre.Enabled = false;
            cbxCréneau.Enabled = false;
            btnNouveauMembre.Visible = false;
            btnModifierMembre.Visible = false;
            btnEnregistrerMembre.Visible = true;
            btnAnnulerMembre.Visible = true;
            cbxLigue.Enabled = false;
            lbxMembres.Enabled = false;
        }

        private void btnEnregistrerMembre_Click(object sender, EventArgs e)
        {
            //Activation / désactivation des boutons nécéssaires
            tbxNomMembre.Enabled = false;
            tbxPrénom.Enabled = false;
            tbxTéléphone.Enabled = false;
            tbxMail.Enabled = false;
            tbxCodeMembre.Enabled = false;
            btnAjouterMembre.Enabled = true;
            btnSupprimerMembre.Enabled = true;
            cbxCréneau.Enabled = true;
            btnNouveauMembre.Visible = true;
            btnModifierMembre.Visible = true;
            btnEnregistrerMembre.Visible = false;
            btnAnnulerMembre.Visible = false;
            cbxLigue.Enabled = true;
            lbxMembres.Enabled = true;

            //Déclaration de l'objet connexion
            OleDbConnection laConnexion = new OleDbConnection();
            String connexString = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=..\\..\\..\\M2L.accdb";
            laConnexion.ConnectionString = connexString;

            OleDbCommand enregistrerMembres = new OleDbCommand(); //Création d'une nouvelle commande
            enregistrerMembres.CommandText = "UPDATE membres " +
                "SET nom = '" + tbxNomMembre.Text 
                + "', prenom = '" + tbxPrénom.Text 
                + "', telephone = '" + tbxTéléphone.Text 
                + "', mail = '" + tbxMail.Text 
                + "' WHERE codeMembre = '" + tbxCodeMembre.Text + "';"; //On effectue la requête SQL nécéssaire dans notre commande
            enregistrerMembres.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            enregistrerMembres.ExecuteNonQuery(); //Execution de la requête
            laConnexion.Close(); //Fermeture de la connexion
        }

        private void btnAnnulerMembre_Click(object sender, EventArgs e)
        {
            //Activation / désactivation des boutons nécéssaires
            tbxNomMembre.Enabled = false;
            tbxNomMembre.Undo();
            tbxPrénom.Enabled = false;
            tbxPrénom.Undo();
            tbxTéléphone.Enabled = false;
            tbxTéléphone.Undo();
            tbxMail.Enabled = false;
            tbxMail.Undo();
            tbxCodeMembre.Enabled = false;
            tbxCodeMembre.Undo();
            btnAjouterMembre.Enabled = true;
            btnSupprimerMembre.Enabled = true;
            cbxCréneau.Enabled = true;
            btnNouveauMembre.Visible = true;
            btnModifierMembre.Visible = true;
            btnEnregistrerMembre.Visible = false;
            btnAnnulerMembre.Visible = false;
            cbxLigue.Enabled = true;
            lbxMembres.Enabled = true;
            btnEnregistrerNouveau.Visible = false;
        }

        private void btnEnregistrerNouveau_Click(object sender, EventArgs e)
        {
            //Activation / désactivation des boutons nécéssaires
            tbxNomMembre.Enabled = false;
            tbxPrénom.Enabled = false;
            tbxTéléphone.Enabled = false;
            tbxMail.Enabled = false;
            tbxCodeMembre.Enabled = false;
            btnAjouterMembre.Enabled = true;
            btnSupprimerMembre.Enabled = true;
            cbxCréneau.Enabled = true;
            btnNouveauMembre.Visible = true;
            btnModifierMembre.Visible = true;
            btnEnregistrerMembre.Visible = false;
            btnAnnulerMembre.Visible = false;
            cbxLigue.Enabled = true;
            lbxMembres.Enabled = true;
            btnEnregistrerNouveau.Visible = false;

            //Déclaration de l'objet connexion
            OleDbConnection laConnexion = new OleDbConnection();
            String connexString = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=..\\..\\..\\M2L.accdb";
            laConnexion.ConnectionString = connexString;

            OleDbCommand enregistrerMembres = new OleDbCommand(); //Création d'une nouvelle commande
            enregistrerMembres.CommandText = "INSERT INTO membres (codeMembre, nom, prenom, telephone, mail, codeLigue) VALUES ('" + tbxCodeMembre.Text + "', '" + tbxNomMembre.Text + "', '" + tbxPrénom.Text + "', '" + tbxTéléphone.Text + "', '" + tbxMail.Text + "', '" + tbxCodeLigue.Text + "');"; //On effectue la requête SQL nécéssaire dans notre commande
            enregistrerMembres.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            enregistrerMembres.ExecuteNonQuery(); //Execution de la requête
            laConnexion.Close(); //Fermeture de la connexion
        }

        private void btnNouveauMembre_Click(object sender, EventArgs e)
        {
            //Activation / désactivation des boutons nécéssaires
            tbxNomMembre.Enabled = true;
            tbxNomMembre.Text = "";
            tbxPrénom.Enabled = true;
            tbxPrénom.Text = "";
            tbxTéléphone.Enabled = true;
            tbxTéléphone.Text = "";
            tbxMail.Enabled = true;
            tbxMail.Text = "";
            tbxCodeMembre.Enabled = true;
            tbxCodeMembre.Text = "";
            btnAjouterMembre.Enabled = false;
            btnSupprimerMembre.Enabled = false;
            cbxCréneau.Enabled = false;
            btnNouveauMembre.Visible = false;
            btnModifierMembre.Visible = false;
            btnEnregistrerNouveau.Visible = true;
            btnAnnulerMembre.Visible = true;
            cbxLigue.Enabled = false;
            lbxMembres.Enabled = false;
        }

        private void btnAjouterMembre_Click(object sender, EventArgs e)
        {
            //Déclaration de l'objet connexion
            OleDbConnection laConnexion = new OleDbConnection();
            String connexString = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=..\\..\\..\\M2L.accdb";
            laConnexion.ConnectionString = connexString;

            OleDbCommand ajouterMembres = new OleDbCommand(); //Création d'une nouvelle commande
            ajouterMembres.CommandText = "INSERT INTO participer (codeMembre, codeCreneau) VALUES ('" + tbxCodeMembre.Text + "', '" + tbxCodeCreneau.Text + "');"; //On effectue la requête SQL nécéssaire dans notre commande
            ajouterMembres.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            ajouterMembres.ExecuteNonQuery(); //Execution de la requête
            laConnexion.Close(); //Fermeture de la connexion

            OleDbCommand listeInscrits = new OleDbCommand(); //Création d'une nouvelle commande
            listeInscrits.CommandText = "select membres.nom from membres,participer where membres.codemembre = participer.codemembre;"; //On effectue la requête SQL nécéssaire dans notre commande
            listeInscrits.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader listeInscritsDR = listeInscrits.ExecuteReader(); //Lecture des données

            lbxInscription.Items.Clear();
            while (listeInscritsDR.Read())
            {
                lbxInscription.Items.Add(listeInscritsDR.GetString(0));
            }
            laConnexion.Close();
        }

        private void btnSupprimerMembre_Click(object sender, EventArgs e)
        {
            //Déclaration de l'objet connexion
            OleDbConnection laConnexion = new OleDbConnection();
            String connexString = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=..\\..\\..\\M2L.accdb";
            laConnexion.ConnectionString = connexString;

            OleDbCommand supprimerMembres = new OleDbCommand(); //Création d'une nouvelle commande
            supprimerMembres.CommandText = "DELETE FROM participer WHERE codeMembre = " + tbxCodeMembre.Text + " ; "; //On effectue la requête SQL nécéssaire dans notre commande
            supprimerMembres.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            supprimerMembres.ExecuteNonQuery(); //Execution de la requête
            laConnexion.Close(); //Fermeture de la connexion
        }

        private void cbxCréneau_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSupprimerMembre.Enabled = true;
            //Déclaration de l'objet connexion
            OleDbConnection laConnexion = new OleDbConnection();
            String connexString = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=..\\..\\..\\M2L.accdb";
            laConnexion.ConnectionString = connexString;

            OleDbCommand codeCreneau = new OleDbCommand(); //Création d'une nouvelle commande
            codeCreneau.CommandText = "select creneau.codecreneau from creneau where libelléCreneau = '" + cbxCréneau.SelectedItem + "';"; //On effectue la requête SQL nécéssaire dans notre commande
            codeCreneau.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader codeCreneauDR = codeCreneau.ExecuteReader(); //Lecture des données

            while (codeCreneauDR.Read())
            {
                tbxCodeCreneau.Text = (codeCreneauDR.GetString(0));
            }
            laConnexion.Close();

            OleDbCommand listeInscrits = new OleDbCommand(); //Création d'une nouvelle commande
            listeInscrits.CommandText = "select membres.nom from membres,participer,creneau where membres.codeMembre = participer.codeMembre and creneau.codecreneau = participer.codecreneau and libelléCreneau = '" + cbxCréneau.SelectedItem + "';"; //On effectue la requête SQL nécéssaire dans notre commande
            listeInscrits.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader listeInscritsDR = listeInscrits.ExecuteReader(); //Lecture des données

            lbxInscription.Items.Clear();
            while (listeInscritsDR.Read())
            {
                lbxInscription.Items.Add(listeInscritsDR.GetString(0));
            }
            laConnexion.Close();
            
        }

        private void lbxInscription_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Déclaration de l'objet connexion
            OleDbConnection laConnexion = new OleDbConnection();
            String connexString = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=..\\..\\..\\M2L.accdb";
            laConnexion.ConnectionString = connexString;

            //Récupération des noms depuis tbxNomMembre
            tbxNomMembre.Text = Convert.ToString(lbxInscription.SelectedItem);

            //Récupération des prénoms depuis la BDD
            OleDbCommand prenomMembres = new OleDbCommand(); //Création d'une nouvelle commande
            prenomMembres.CommandText = "select prenom from membres where nom = '" + lbxInscription.SelectedItem + "';"; //On effectue la requête SQL nécéssaire dans notre commande
            prenomMembres.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader prenomMembresDataReader = prenomMembres.ExecuteReader(); //Lecture des données

            //Ecriture dans tbxPrénom
            while (prenomMembresDataReader.Read())
            {
                tbxPrénom.Text = (prenomMembresDataReader.GetString(0));
            }
            laConnexion.Close();

            //Récupération des n° de téléphone depuis la BDD
            OleDbCommand telMembres = new OleDbCommand(); //Création d'une nouvelle commande
            telMembres.CommandText = "select telephone from membres where nom = '" + lbxInscription.SelectedItem + "';"; //On effectue la requête SQL nécéssaire dans notre commande
            telMembres.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader telMembresDataReader = telMembres.ExecuteReader(); //Lecture des données

            //Ecriture dans tbxTelephone
            while (telMembresDataReader.Read())
            {
                tbxTéléphone.Text = (telMembresDataReader.GetString(0));
            }
            laConnexion.Close();

            //Récupération des mails depuis la BDD
            OleDbCommand mailMembres = new OleDbCommand(); //Création d'une nouvelle commande
            mailMembres.CommandText = "select mail from membres where nom = '" + lbxInscription.SelectedItem + "';"; //On effectue la requête SQL nécéssaire dans notre commande
            mailMembres.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader mailMembresDataReader = mailMembres.ExecuteReader(); //Lecture des données

            while (mailMembresDataReader.Read())
            {
                tbxMail.Text = (mailMembresDataReader.GetString(0));
            }
            laConnexion.Close();

            //Récupération du code membre depuis la BDD
            OleDbCommand codeMembre = new OleDbCommand(); //Création d'une nouvelle commande
            codeMembre.CommandText = "select codeMembre from membres where nom = '" + lbxInscription.SelectedItem + "';"; //On effectue la requête SQL nécéssaire dans notre commande
            codeMembre.Connection = laConnexion; //Jointure
            laConnexion.Open(); //Ouverture de la connexion
            OleDbDataReader codeMembreDR = codeMembre.ExecuteReader(); //Lecture des données

            //Ecriture dans tbxCodeMembre
            while (codeMembreDR.Read())
            {
                tbxCodeMembre.Text = (codeMembreDR.GetString(0));
            }
            laConnexion.Close();
            lbxInscription.Refresh();
        }
    }
}
