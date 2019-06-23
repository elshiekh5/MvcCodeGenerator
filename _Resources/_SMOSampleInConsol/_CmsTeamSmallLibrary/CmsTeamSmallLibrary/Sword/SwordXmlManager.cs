using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Hindawi.OnlinePlatform.BLL;
using Hindawi.OnlinePlatform.BLL.FromDb;

namespace CmsTeamSmallLibrary.Sword
{
    public class SwordXmlManager
    {
        #region --------------BuildMetaDataXmlContents--------------
        //------------------------------------------------------------------
        //BuildMetaDataXmlContents
        //------------------------------------------------------------------
        public static string BuildMetaDataXmlContents(Article currentArticle, string OwnerName, string PdfFile)
        {
            //-------------------------------------------------------------
            //Load Mets standards
            //-------------------------------------------------------------
            StreamReader MetsReferanceStream = new StreamReader("_XMlReferances/MetsReferance.xml", Encoding.UTF8);
            string metsContents = null;
            metsContents = MetsReferanceStream.ReadToEnd();
            MetsReferanceStream.Close();
            //-------------------------------------------------------------
            //Load Author node
            //-------------------------------------------------------------
            StreamReader MetsAutorsBlockStream = new StreamReader("_XMlReferances/MetsAutorsBlock.xml", Encoding.UTF8);
            string AutorsBlock = null;
            AutorsBlock = MetsAutorsBlockStream.ReadToEnd();
            MetsAutorsBlockStream.Close();
            //-------------------------------------------------------------
            metsContents = metsContents.Replace("[OwnerName]", OwnerName);
            metsContents = metsContents.Replace("[DateTimeNow]", DateTime.Now.ToString("s"));
            metsContents = metsContents.Replace("[ArticleTitle]", Utilities.HtmlEncodeText(Utilities.RemoveMathMlAndOtherChildNodes(currentArticle.ArticleTitle)));
            metsContents = metsContents.Replace("[ArticleAbstract]", Utilities.HtmlEncodeText(Utilities.RemoveMathMlAndOtherChildNodes(currentArticle.ArticleAbstract(true))));
            ArrayList AuthArray = ArticlesAuthor.GetAuthors(currentArticle.Pii);
            string authorTages = "";
            foreach (ArticlesAuthor auth in AuthArray)
            {
                authorTages += AutorsBlock.Replace("[AuthorName]", auth.Surname + ", " + auth.GivenName);
            }
            metsContents = metsContents.Replace("[AuthorsBlock]", authorTages);
            metsContents = metsContents.Replace("[ArticleDate]", currentArticle.Date_Archival.Year + "-" + currentArticle.Date_Archival.Date.Month + "-" + currentArticle.Date_Archival.Date.Day);
            metsContents = metsContents.Replace("[CopyRight]", currentArticle.CopyrightHolder + currentArticle.License.PadLeft(currentArticle.License.Length + 1, ' ').Replace("<a rel=\"license\" href=\"http://creativecommons.org/licenses/by/3.0/\">", "").Replace("</a>", ""));
            metsContents = metsContents.Replace("[Citation]", GetArticleCitation(currentArticle));
            metsContents = metsContents.Replace("[PdfFile]", PdfFile);
           
            return metsContents;
        }
        //------------------------------------------------------------------
        #endregion

        #region --------------GetArticleCitation--------------
        //------------------------------------------------------------------
        //GetArticleCitation
        //------------------------------------------------------------------
        public static string GetArticleCitation(Article article)
        {
            //string runningAuthor;
            //string authorsFullNamesWithEmails;          
            //string authorsFullNames = Hindawi.OnlinePlatform.BLL.FromDb.ArticlesAuthor.PersonFullNames(Hindawi.OnlinePlatform.BLL.FromDb.ArticlesAuthor.GetAuthors(article.Pii), "author", false, null, out runningAuthor, out authorsFullNamesWithEmails);

            string authorsBackwordNamesWithEtal = "";
            string authorsBackwordNamesWithOthers = "";

            ArticlesAuthor.PersonBackwordNames(ArticlesAuthor.GetAuthors(article.Pii), out authorsBackwordNamesWithEtal, out authorsBackwordNamesWithOthers);
            string articleCite;

            articleCite = "";
            if (!string.IsNullOrEmpty(authorsBackwordNamesWithEtal))
                articleCite += authorsBackwordNamesWithEtal + ", ";
            articleCite += "&#8220;" + article.Title(true).Replace("<small.letters", "<span style=\"text-transform: lowercase;\"").Replace("</small.letters", "</span") + ",&#8221;";
            articleCite += " " + article.JournalFullTitle;
            articleCite += ", vol. " + article.VolumeNumber + ", ";
            if (!string.IsNullOrEmpty(article.IssueNumber))
                articleCite += "no. " + article.IssueNumber + ", ";

            if (string.IsNullOrEmpty(article.ArticleId))
                articleCite += "pp. " + article.Fpage + "-" + article.Lpage + "";
            else
                articleCite += "Article ID " + article.ArticleId + ", " + article.PageCount + " pages";

            articleCite += ", " + article.PublicationYear + ". doi:" + article.Doi;
            articleCite += "";
            return articleCite;
        }
        //------------------------------------------------------------------
        #endregion
    }
}
