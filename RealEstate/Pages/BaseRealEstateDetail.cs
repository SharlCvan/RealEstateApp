using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public class BaseRealEstateDetail : ComponentBase
    {

        public Propertys RealEstate { get; set; } = new Propertys();

        [Inject]
        public IRealEstateService RealEstateService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public string UserName { get; set; }

        public Comment NewComment { get; set; } = new Comment();

        public List<Comment> RealEstateComments { get; set; } = new List<Comment>();

        public CommentsPaging commentsPaging { get; set; } = new CommentsPaging();

        public int totalpages;

        public int currentPage = 1;

        public int QuantityPerPAge = 4;

        public async Task SelectedPage(int page)
        {
            currentPage = page;
            await LoadComments(page);
        }

        public async Task LoadComments(int page = 1, int quantityPerPage = 4)
        {
            //TODO: Ta bort null hantering & ta bort CommentsPaging klass och returnera bara comments och sen pages count i separata metoder
            //Kan trycka next även om det inte finns någon kommentar, Ladda kommentarer efter postad kommentar
            commentsPaging = await RealEstateService.GetRealEstateComments(Id, page, quantityPerPage);

            try
            {
                RealEstateComments = commentsPaging.Comments;
                totalpages = (int)Math.Ceiling((decimal)RealEstateService.GetTotalRealEstateCommentsPages(int.Parse(Id)).Result / QuantityPerPAge);
            }
            catch(NullReferenceException n)
            {

            }
            catch(Exception e)
            {

            }
        }
        //TODO: Ta bort null hantering
        protected async override Task OnInitializedAsync()
        {
            NewComment.Errors = new List<string>();
            RealEstate.Urls = new List<string>();
            RealEstate = await RealEstateService.GetRealEstate(int.Parse(Id));
            
            try
            {
                RealEstate.Urls.Add(RealEstate.ListingURL);
                UserName = RealEstate.UserName;
                await LoadComments();
            }
            catch(NullReferenceException n)
            {

            }
            catch(Exception e)
            {

            }
             
        }

        public async Task OnPostComment(PostedComment postedComment)
        {
            postedComment.RealEstateId = int.Parse(Id);
            NewComment = await RealEstateService.PostComment(postedComment);

            if(NewComment.IsSuccesfullCommentPost)
            {
                StateHasChanged();
            }
        }

    }
    
}
