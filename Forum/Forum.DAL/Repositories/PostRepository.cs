using System.Data;
using Dapper;
using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using Npgsql;

namespace Forum.DAL.Repositories;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(NpgsqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "posts")
    {
    }

    override public async Task<Post> GetAsync(int id)
    {
        var sql = @"Select p.id, p.title, p.content, p.created_at, p.updated_at, p.user_id, p.category_id, u.id as User_Id, u.id, u.username, u.email, u.created_at, c.id as Category_Id, c.id, c.category_name, c.description 
                    from posts p 
                    Inner join users u on p.user_id = u.id
                    Left join categories c on p.category_id = c.id
                    Where p.id = @id";
        
        var post_result = await _sqlConnection.QueryAsync<Post, User, Category, Post>(sql, 
            (post, user, category)=>
            {
                post.user = user;
                post.category = category;
                return post;
            },
        param: new {id = id},
        splitOn: "User_Id, Category_Id");
        var post = post_result.FirstOrDefault();

        sql = @"Select c.id, c.content, c.created_at, c.post_id, c.user_id, u.id as User_Id, u.id, u.username, u.email, u.created_at 
                From comments c
                Inner join users u on c.user_id = u.id
                Where c.post_id = @id
                Order by c.created_at";
        
        var comment_result = await _sqlConnection.QueryAsync<Comment, User, Comment>(sql, (comment, user) =>
            {
                comment.user = user;
                return comment;
            },
            param: new {id = id},
            splitOn: "User_Id"
            );
        post.comments = comment_result.ToList();
        
        return post;
        //var sql = @"Select * From posts p 
        //            Inner Join users u On p.user_id = u.id
        //            Left Join categories cat On p.category_id = cat.id
        //            Left Join comments c On c.post_id = p.id
        //            Where p.id = 1";

        //var postDictionary = new Dictionary<int, ResponsePost>();
        //
        //var result_post = await _sqlConnection.QueryAsync<ResponsePost, User, Category, Comment, ResponsePost>("Select * From get_post_info(1)", (post, user, category, comment) =>
        //{
        //    ResponsePost postEntry;

        //    if (!postDictionary.TryGetValue(post.id, out postEntry))
        //    {
        //        postEntry = post;
        //        postEntry.user = user;
        //        postEntry.category = category;
        //        postEntry.comments = new List<Comment>();
        //        postDictionary.Add(postEntry.id, postEntry);
        //    }

        //    postEntry.comments.Append(comment);
        //    return postEntry;

        //},
        //    splitOn: "id2,id3,id4",
        //    commandType: CommandType.Text);
        //result_post.ToList();

        // var posts = await _sqlConnection.QueryAsync<Post2, User, Comment2, User, Post2>(sql, (post, post_user, comment, comment_user) => {
        //         comment.user = comment_user;
        //         post.comments.Append(comment);
        //         post.user = post_user;
        //         return post;
        //     }, 
        //     splitOn: "post_id" );

        //posts.ToList().ForEach(product => Console.WriteLine($"Product: {product.ProductName}, Category: {product.Category.CategoryName}"));
        //Console.WriteLine(result_post);
        //return result_post;

        //Console.ReadLine();
    }


    public override async Task<IEnumerable<Post>> GetAllAsync()
    {
        var sql = @"Select p.id, p.title, p.content, p.created_at, p.updated_at, p.user_id, p.category_id, u.id as User_Id, u.id, u.username, u.email, u.created_at, c.id as Category_Id, c.id, c.category_name, c.description 
                    from posts p 
                    Inner join users u on p.user_id = u.id
                    Left join categories c on p.category_id = c.id";
        
        var posts = await _sqlConnection.QueryAsync<Post, User, Category, Post>(sql, 
            (post, user, category)=>
            {
                post.user = user;
                post.category = category;
                return post;
            },
        splitOn: "User_Id, Category_Id");
        return posts;
    }
}