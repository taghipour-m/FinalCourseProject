using Dtx;
using System;
using System.Linq;

namespace MyApplication.Controllers

{
    [Microsoft.AspNetCore.Mvc.ApiController]
    [Microsoft.AspNetCore.Mvc.Route
        (template: Infrastructure.RouterConstants.Controller)]
///<summary>
/// Main Controller
///</summary>
public class BookController : Infrastructure.ApiControllerBase
{
    #region Mock Books
    private static System.Collections.Generic.IList<Models.Book> _books;

    public static System.Collections.Generic.IList<Models.Book> Books
    {
        get
        {
            if (_books == null)
            {
                _books =
                    new System.Collections.Generic.List<Models.Book>();

                for (int index = 1; index <= 5; index++)
                {
                    Models.Book user =
                        new Models.Book
                        {
                            BookId = index,
                            BookName = $"BookName { index }",
                            ISBN = $"ISBN { index + 10 }",
                        };

                    _books.Add(user);
                }
            }

            return _books;
        }
    }
    #endregion /Mock Users

    /// <summary>
    /// default constractor
    /// </summary>
    public BookController() : base()
    {
    }

    #region GetAll Objects
    /// <summary>
    /// Get All
    /// </summary>
    /// <returns>Books List</returns>
    [Microsoft.AspNetCore.Mvc.HttpGet]

    [Microsoft.AspNetCore.Mvc.ProducesResponseType
        (type: typeof(Dtx.Result<System.Collections.Generic.IList<Models.Book>>),
        statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
    [Microsoft.AspNetCore.Mvc.ProducesResponseType
        (type: typeof(Dtx.Result<System.Collections.Generic.IList<Models.Book>>),
        statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
    public
        async
        System.Threading.Tasks.Task
        <Dtx.Result<System.Collections.Generic.IList<Models.Book>>>
        GetAllAsync()
    {
        var result =
            new FluentResults.Result<System.Collections.Generic.IList<Models.Book>>();

        await System.Threading.Tasks.Task.Run(() =>
        {
            try
            {
                System.Collections.Generic.IList<Models.Book> list = null;

                list =
                    Books
                    .OrderBy(current => current.BookName)
                    .ToList()
                    ;
                if (list != null && list.Count > 0)
                    result.WithValue(value: list);
                else
                    throw new System.Exception(message: "There are no recordes!");
            }
            catch (System.Exception ex)
            {
                // Log the Error in DB

                result.WithError(errorMessage: ex.Message);
            }
        });

        return result.ConvertToDtxResult();
    }
    #endregion /GetAsync Method

    #region Get one object By Id
    /// <summary>
    /// Get one by Id
    /// </summary>
    /// <returns>A List with one record</returns>
    [Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}")]

    [Microsoft.AspNetCore.Mvc.ProducesResponseType
        (type: typeof(Dtx.Result<System.Collections.Generic.IList<Models.Book>>),
        statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
    [Microsoft.AspNetCore.Mvc.ProducesResponseType
        (type: typeof(Dtx.Result<System.Collections.Generic.IList<Models.Book>>),
        statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
    public
        async
        System.Threading.Tasks.Task
        <Dtx.Result<System.Collections.Generic.IList<Models.Book>>>
        GetByIdAsync(Int32 id)
    {
        var result =
            new FluentResults.Result<System.Collections.Generic.IList<Models.Book>>();

        Models.Book book = null;

        await System.Threading.Tasks.Task.Run(() =>
        {
            try
            {
                System.Collections.Generic.IList<Models.Book> list = null;

                book =
                    Books
                    .Where(current => current.BookId == id)
                    .FirstOrDefault();

                if (book != null)
                {
                    list.Add(book);
                    result.WithValue(value: list);
                }
                else
                    throw new System.Exception(message: "Not Found!");
            }
            catch (System.Exception ex)
            {
                // Log the Error in DB

                result.WithError(errorMessage: ex.Message);
            }
        });

        return result.ConvertToDtxResult();
    }
    #endregion /GetAllAsync Method

    #region Create new object
    /// <summary>
    /// Insert by input
    /// </summary>
    /// <returns>Book List with new record</returns>
    [Microsoft.AspNetCore.Mvc.HttpPost]

    [Microsoft.AspNetCore.Mvc.ProducesResponseType
        (type: typeof(Dtx.Result<System.Collections.Generic.IList<Models.Book>>),
        statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
    [Microsoft.AspNetCore.Mvc.ProducesResponseType
        (type: typeof(Dtx.Result<System.Collections.Generic.IList<Models.Book>>),
        statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
    public
        async
        System.Threading.Tasks.Task
        <Dtx.Result<System.Collections.Generic.IList<Models.Book>>>
        InsertAsync([Microsoft.AspNetCore.Mvc.FromBody]
                      ViewModels.Users.PostRequestViewModel viewModel)
    {
        var result =
            new FluentResults.Result<System.Collections.Generic.IList<Models.Book>>();

        Models.Book newBook = null;

        await System.Threading.Tasks.Task.Run(() =>
        {
            try
            {
                System.Collections.Generic.IList<Models.Book> list = null;

                int newId =
                    Books.Max(current => current.BookId) + 1;

                newBook =
                    new Models.Book
                    {
                        BookId = newId,
                        BookName = viewModel.BookName,
                        ISBN = viewModel.ISBN,
                    };

                Books.Add(newBook);

                if (newBook != null)
                {
                    result.WithValue(value: Books);
                } 
                else
                    throw new System.Exception(message: "Register Failed!");
            }
            catch (System.Exception ex)
            {
                // Log the Error in DB

                result.WithError(errorMessage: ex.Message);
            }
        });

        return result.ConvertToDtxResult();
    }
    #endregion /insert Async Method

    #region Update An Object
    /// <summary>
    /// Update by Id
    /// </summary>
    /// <returns>Book List with one record</returns>
    [Microsoft.AspNetCore.Mvc.HttpPut(template: "{id}")]

    [Microsoft.AspNetCore.Mvc.ProducesResponseType
        (type: typeof(Dtx.Result<System.Collections.Generic.IList<Models.Book>>),
        statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
    [Microsoft.AspNetCore.Mvc.ProducesResponseType
        (type: typeof(Dtx.Result<System.Collections.Generic.IList<Models.Book>>),
        statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
    public
        async
        System.Threading.Tasks.Task
        <Dtx.Result<System.Collections.Generic.IList<Models.Book>>>
        UpdateByIdAsync(Int32 id, [Microsoft.AspNetCore.Mvc.FromBody]
                          ViewModels.Users.PostRequestViewModel viewModel)
    {
        var result =
          new FluentResults.Result<System.Collections.Generic.IList<Models.Book>>();

        Models.Book book = null;

        await System.Threading.Tasks.Task.Run(() =>
        {
            try
            {
                System.Collections.Generic.IList<Models.Book> list = null;

                book =
                    Books
                    .Where(current => current.BookId == id)
                    .FirstOrDefault();

                if (book != null)
                {
                    book.BookName = viewModel.BookName;
                    book.ISBN = viewModel.ISBN;
                    result.WithValue(value: Books);
                }
                    
                else
                    throw new System.Exception(message: "Not Found!");
            }
            catch (System.Exception ex)
            {
                // Log the Error in DB

                result.WithError(errorMessage: ex.Message);
            }
        });

        return result.ConvertToDtxResult();
    }

    #endregion / update

    #region Delete An Object
    /// <summary>
    /// delete one object by Id
    /// </summary>
    /// <returns>List without deleted object</returns>
    [Microsoft.AspNetCore.Mvc.HttpDelete(template: "{id}")]

    [Microsoft.AspNetCore.Mvc.ProducesResponseType
    (type: typeof(Dtx.Result<System.Collections.Generic.IList<Models.Book>>),
    statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
    [Microsoft.AspNetCore.Mvc.ProducesResponseType
    (type: typeof(Dtx.Result<System.Collections.Generic.IList<Models.Book>>),
    statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
    public
    async
    System.Threading.Tasks.Task
    <Dtx.Result<System.Collections.Generic.IList<Models.Book>>>
    DeleteByIdAsync(Int32 id)
    {
        var result =
            new FluentResults.Result<System.Collections.Generic.IList<Models.Book>>();

        Models.Book book = null;

        await System.Threading.Tasks.Task.Run(() =>
        {
            try
            {
                System.Collections.Generic.IList<Models.Book> list = null;

                book =
                    Books
                    .Where(current => current.BookId == id)
                    .FirstOrDefault();
                Books.Remove(book);

                if (book != null)
                    result.WithValue(value: Books); // result without deleted object
                else
                    throw new System.Exception(message: "Not Found!");
            }
            catch (System.Exception ex)
            {
                // Log the Error in DB

                result.WithError(errorMessage: ex.Message);
            }
        });

        return result.ConvertToDtxResult();
    }
    #endregion
}
}
