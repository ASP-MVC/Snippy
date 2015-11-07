namespace Snippy.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Snippy.Common;
    using Snippy.Models;

    public sealed class Configuration : DbMigrationsConfiguration<SnippyDbContext>
    {
        private const string AdminUserName = "admin";
        private const string ColonAndSpace = ": ";
        private const string CommaAndSpace = ", ";

        private UserStore<User> store;

        private UserManager<User> manager;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            // TODO change to false 
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SnippyDbContext context)
        {
            this.SeedUsers(context);
            this.SeedRoles(context);
            this.SeedLabels(context);
            this.SeedLanguages(context);
            this.SeedSnippets(context);
            this.SeedComments(context);
        }

        private void SeedLanguages(SnippyDbContext context)
        {
            if (context.ProgrammingLanguages.Any())
            {
                return;
            }
            var languagesSource = "C#, JavaScript, Python, CSS, SQL, PHP";
            var languages = this.SplitRowBy(CommaAndSpace, languagesSource);

            foreach (var languageAsString in languages)
            {
                context.ProgrammingLanguages.AddOrUpdate(new ProgrammingLanguage
                {
                    Name = languageAsString
                });
            }
            context.SaveChanges();
        }

        private void SeedLabels(SnippyDbContext context)
        {
            if (context.Labels.Any())
            {
                return;
            }
            var labelsSource =
                "bug, funny, jquery, mysql, useful, web, geometry, back-end, front-end, games";

            var labels = this.SplitRowBy(CommaAndSpace, labelsSource);

            foreach (var labelAsString in labels)
            {
                context.Labels.AddOrUpdate(new Label
                {
                    Text = labelAsString
                });
            }
            context.SaveChanges();
        }

        private void SeedRoles(SnippyDbContext context)
        {
            if (!context.Roles.Any() && context.Users.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var adminRole = new IdentityRole { Name = GlobalConstants.AdminRole };
                roleManager.Create(adminRole);
                this.store = new UserStore<User>(context);
                this.manager = new UserManager<User>(this.store);
                var adminUser = context.Users.FirstOrDefault(x => x.UserName == AdminUserName);
                this.manager.AddToRole(adminUser.Id, GlobalConstants.AdminRole);
                context.SaveChanges();
            }
        }

        private void SeedUsers(SnippyDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }
            this.store = new UserStore<User>(context);
            this.manager = new UserManager<User>(this.store);
            var usersList = new List<User>
            {
                new User { UserName = "admin", Email = "admin@snippy.softuni.com" },
                new User { UserName = "someUser", Email = "someUser@example.com" },
                new User { UserName = "newUser", Email = "new_user@gmail.com" },
            };
            var usersPassword = new List<string>()
            {
                "adminPass123",
                "someUserPass123",
                "userPass123"
            };

            for (int i = 0; i < usersList.Count; i++)
            {
                this.manager.Create(usersList[i], usersPassword[i]);
            }
            
            context.SaveChanges();
        }

        private void SeedComments(SnippyDbContext context)
        {
            if (context.Comments.Any())
            {
                return;
            }
            // Add builded comments to list
            #region
            var commentsList = new List<Comment>
            {
                this.BuildComment(
                    "Now that's really funny! I like it!",
                    "admin",
                    "30.10.2015 11:50:38",
                    "Ternary Operator Madness",
                    context),
                this.BuildComment(
                    "Here, have my comment!",
                    "newUser",
                    "01.11.2015 15:52:42",
                    "Ternary Operator Madness",
                    context),
                this.BuildComment(
                    "I didn't manage to comment first :(",
                    "someUser",
                    "02.11.2015 05:30:20",
                    "Ternary Operator Madness",
                    context),
                this.BuildComment(
                    "That's why I love Python - everything is so simple!",
                    "newUser",
                    "27.10.2015 15:28:14",
                    "Reverse a String",
                    context),
                this.BuildComment(
                    "I have always had problems with Geometry in school. Thanks to you I can now do this without ever having to learn this damn subject",
                    "someUser",
                    "29.10.2015 15:08:42",
                    "Points Around A Circle For GameObject Placement",
                    context),
                this.BuildComment(
                    "Thank you. However, I think there must be a simpler way to do this. I can't figure it out now, but I'll post it when I'm ready.",
                    "admin",
                    "03.11.2015 12:56:20",
                    "Numbers only in an input field",
                    context),
            };
#endregion
            foreach (var comment in commentsList)
            {
                context.Comments.AddOrUpdate(comment);
            }

            context.SaveChanges();
        }

        private Comment BuildComment(
            string content,
            string authorUsername,
            string creationDateAsString,
            string snippetTitle,
            SnippyDbContext context)
        {
            var authorId = context.Users.Where(x => x.UserName == authorUsername).Select(x => x.Id).FirstOrDefault();
            var creationTime = DateTime.Parse(creationDateAsString);
            var snippetId = context.Snippets.Where(x => x.Title == snippetTitle).Select(x => x.Id).FirstOrDefault();
            var comment = new Comment
            {
                Content = content,
                AuthorId = authorId,
                CreatedAt = creationTime,
                SnippetId = snippetId
            };
            return comment;
        }

        private void SeedSnippets(SnippyDbContext context)
        {
            if (context.Snippets.Any())
            {
                return;
            }
            // Filling List with snippets
            #region
                        var snippetsList = new List<Snippet>()
                        {
                            this.BuildSnippet(
                                "Ternary Operator Madness",
                                "This is how you DO NOT user ternary operators in C#!",
                                "bool X = Glob.UserIsAdmin ? true : false;",
                                "admin",
                                "26.10.2015 17:20:33",
                                "C#",
                                "funny",
                                context),
                            this.BuildSnippet(
                                "Points Around A Circle For GameObject Placement",
                                "Determine points around a circle which can be used to place elements around a central point",
                                @"private Vector3 DrawCircle(float centerX, float centerY, float radius, float totalPoints, float currentPoint)
                                {
	                                float ptRatio = currentPoint / totalPoints;
	                                float pointX = centerX + (Mathf.Cos(ptRatio * 2 * Mathf.PI)) * radius;
	                                float pointY = centerY + (Mathf.Sin(ptRatio * 2 * Mathf.PI)) * radius;

	                                Vector3 panelCenter = new Vector3(pointX, pointY, 0.0f);

	                                return panelCenter;
                                }",
                                "admin",
                                "26.10.2015 20:15:30",
                                "C#",
                                "geometry, games",
                                context),
                            this.BuildSnippet(
                                "forEach. How to break?",
                                "Array.prototype.forEach You can\'t break forEach. So use \"some\" or \"every\". Array.prototype.some some is pretty much the same as forEach but it break when the callback returns true. Array.prototype.every every is almost identical to some except it's expecting false to break the loop.",
                                @"var ary = [JavaScript, Java, CoffeeScript, TypeScript]
                                ary.some(function (value, index, _ary) {
                                    console.log(index + ': ' + value);
                                            return value === 'CoffeeScript';
                                        });
                                // output: 
                                // 0: JavaScript
                                // 1: Java
                                // 2: CoffeeScript
 
                                ary.every(function(value, index, _ary)
                                        {
                                            console.log(index + ' : ' + value);
                                            return value.indexOf('Script') > -1;
                                        });
                                // output:
                                // 0: JavaScript
                                // 1: Java
                                ",
                                "newUser",
                                "27.10.2015 10:53:20",
                                "JavaScript",
                                "jquery, useful, web, front-end",
                                context),
                            this.BuildSnippet(
                                "Numbers only in an input field",
                                "Method allowing only numbers (positive / negative / with commas or decimal points) in a field",
                                @"$('#input').keypress(function(event){
	                            var charCode = (event.which) ? event.which : window.event.keyCode;
	                            if (charCode <= 13) { return true; } 
	                            else {
                                        var keyChar = String.fromCharCode(charCode);
                                        var regex = new RegExp('[0-9,.-]');
                                        return regex.test(keyChar);
                                    }
                                    });",
                                "someUser",
                                "28.10.2015 09:00:56",
                                "JavaScript",
                                "web, front-end",
                                context),
                             this.BuildSnippet(
                                "Create a link directly in an SQL query",
                                "That's how you create links - directly in the SQL!",
                                @"SELECT DISTINCT
                                b.Id,
                                concat('<button type=""button"" onclick=""DeleteContact(', cast(b.Id as char), ')''>Delete...</button>') as lnkDelete
                                FROM tblContact   b
                                WHERE ....",
                                "admin",
                                "30.10.2015 11:20:00",
                                "SQL",
                                "bug, funny, mysql",
                                context),
                              this.BuildSnippet(
                                "Reverse a String",
                                "Almost not worth having a function for...",
                                @"def reverseString(s):
	                            '''Reverses a string given to it.'''
                                return s[::- 1]",
                                "admin",
                                "26.10.2015 09:35:13",
                                "Python",
                                "useful",
                                context),
                              this.BuildSnippet(
                                "Pure CSS Text Gradients",
                                "This code describes how to create text gradients using only pure CSS",
                                @"/* CSS text gradients */
                                h2[data-text] {
	                                position: relative;
                                }
                                h2[data-text]::after {
	                                content: attr(data-text);
	                                z-index: 10;
	                                color: #e3e3e3;
	                                position: absolute;
	                                top: 0;
	                                left: 0;
	                                -webkit-mask-image: -webkit-gradient(linear, left top, left bottom, from(rgba(0,0,0,0)), color-stop(50%, rgba(0,0,0,1)), to(rgba(0,0,0,0)));
                                ",
                                "someUser",
                                "22.10.2015 19:26:42",
                                "CSS",
                                "web, front-end",
                                context),
                              this.BuildSnippet(
                                "Check for a Boolean value in JS",
                                "How to check a Boolean value - the wrong but funny way :D",
                                @"var b = true;
                                if (b.toString().length < 5) {
                                  //...
                                }",
                                "admin",
                                "22.10.2015 05:30:04",
                                "JavaScript",
                                "funny",
                                context),
                        };
            #endregion

            foreach (var snippet in snippetsList)
            {
                context.Snippets.AddOrUpdate(snippet);
            }
            context.SaveChanges();
        }

        private Snippet BuildSnippet(
            string title,
            string description,
            string code,
            string authorUsername,
            string creationTimeAsString,
            string languageName,
            string labelsRow,
            SnippyDbContext context)
        {
            var authorId = context.Users.Where(x => x.UserName == authorUsername).Select(x => x.Id).FirstOrDefault();
            var creationTime = DateTime.Parse(creationTimeAsString);
            var languageId = context.ProgrammingLanguages.Where(x => x.Name == languageName).Select(x => x.Id).FirstOrDefault();
            var labelsArr = this.SplitRowBy(CommaAndSpace, labelsRow);
            var getLabels = this.GetLabels(labelsArr, context);
            var snippet = new Snippet
            {
                Title =title,
                Description = description,
                AuthorId = authorId,
                CreatedAt = creationTime,
                ProgrammingLanguageId = languageId,
                Labels = getLabels,
                Code = code
            };
            return snippet;
        }

        private ICollection<Label> GetLabels(string[] labelsArr, SnippyDbContext context)
        {
            var labels = new Collection<Label>();
            foreach (var labelAsString in labelsArr)
            {
                var label = context.Labels.FirstOrDefault(x => x.Text == labelAsString);
                labels.Add(label);
            }
            return labels;
        } 

        private string[] SplitRowBy(string separator, string row)
        {
            var splittedRow =
                row.Split(
                    new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            return splittedRow;
        }
    }
}
