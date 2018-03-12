using System.Collections.Generic;
using SearcherAPI.Models;

namespace SearcherAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SearcherAPI.Models.SearcherContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            Database.SetInitializer<SearcherAPI.Models.SearcherContext>(new CreateDatabaseIfNotExists<SearcherContext>());
        }

        protected override void Seed(SearcherAPI.Models.SearcherContext context)
        {
            List<SearchWords> sWordList1 = new List<SearchWords>();
            List<SearchWords> sWordList2 = new List<SearchWords>();

            SearchWords sWord1 = new SearchWords()
            {
                SearchWord = "Lorem ipsum 1"
            };

            SearchWords sWord2 = new SearchWords()
            {
                SearchWord = "jibberish"
            };
            SearchWords sWord3 = new SearchWords()
            {
                SearchWord = "Lorem ipsum 2"
            };

            SearchWords sWord4 = new SearchWords()
            {
                SearchWord = "Sludder"
            };

            context.SearchWords.Add(sWord1);
            context.SearchWords.Add(sWord2);
            context.SearchWords.Add(sWord3);
            context.SearchWords.Add(sWord4);

            sWordList1.Add(sWord1);
            sWordList1.Add(sWord2);
            sWordList2.Add(sWord3);
            sWordList2.Add(sWord4);





            Texts text1 = new Texts()
            {
                title = "Lorem ipsum no. 1",
                text = "Lorem ipsum dolor sit amet elementum. A tempus vestibulum dapibus " +
                       "id morbi consequat faucibus libero cursus nunc malesuada. " +
                       "Vulputate ac et. Mi dictum quas. Laoreet aenean risus lacus sed sapien.",
                SearchWords = sWordList1

            };

            Texts text2 = new Texts()
            {
                title = "Lorem ipsum no. 2",
                text = "Leo et sollicitudin. Eu nunc lacus. Mauris elementum suscipit " +
                       "integer augue sed fringilla lacus a. Scelerisque venenatis at mollis " +
                       "eros dolor. Ut ut tellus nec arcu sodales tellus massa sem.",
                SearchWords = sWordList2
            };

            context.Texts.Add(text1);
            context.Texts.Add(text2);
        }
    }
}
