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

        public List<string> Errors { get; set; } = new List<string>();

        public CommentsPaging commentsPaging { get; set; } = new CommentsPaging();

        public int countOnAfterRender { get; set; }

        public int Totalpages { get; set; }

        public int currentPage = 1;

        public int QuantityPerPAge = 4;

        public async Task SelectedPage(int page)
        {
            currentPage = page;
            await LoadComments(page);
        }

        public async Task LoadComments(int page = 1, int quantityPerPage = 4)
        {

            try
            {
                RealEstateComments = await RealEstateService.GetRealEstateComments(Id, page, quantityPerPage);
            }
            catch(NullReferenceException n)
            {

            }
            catch(Exception e)
            {

            }
        }

        protected async override Task OnInitializedAsync()
        {
            NewComment.errors = new Dictionary<string, string[]>();
            RealEstate.Urls = new List<URL>();

            RealEstate = await RealEstateService.GetRealEstate(int.Parse(Id));

            foreach (var error in RealEstate.Errors)
            {
                Errors.Add(error.Value[0]);
            }
            
            try
            {
                RealEstate.Urls.Add(new URL(RealEstate.ListingURL));
                UserName = RealEstate.UserName;

                Totalpages = (int)Math.Ceiling((decimal)await RealEstateService.GetTotalRealEstateComments(int.Parse(Id)) / QuantityPerPAge);
                await LoadComments();
            }
            catch
            {

            }
             
        }

        public async Task OnPostComment(PostedComment postedComment)
        {
            postedComment.RealEstateId = int.Parse(Id);
            NewComment = await RealEstateService.PostComment(postedComment);

            if(NewComment.IsSuccesfullCommentPost)
            {
                Totalpages = (int)Math.Ceiling((decimal)await RealEstateService.GetTotalRealEstateComments(int.Parse(Id)) / QuantityPerPAge);
                await SelectedPage(1);
                StateHasChanged();
            }
        }

    }
    
}
