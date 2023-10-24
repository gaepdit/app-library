﻿namespace GaEpd.FileService;

/// <summary>
/// A service for managing file persistence.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Saves a <see cref="Stream"/> as a file. If the file already exists, overwrites the existing file.
    /// </summary>
    /// <param name="stream">The Stream to save.</param>
    /// <param name="fileName">The file name to save the Stream as.</param>
    /// <param name="path">The location where to save the Stream.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    Task SaveFileAsync(Stream stream, string fileName, string path = "", CancellationToken token = default);

    /// <summary>
    /// Checks if file exists.
    /// </summary>
    /// <param name="fileName">The name of the file to look for.</param>
    /// <param name="path">The location of the file.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>`true` if the file exists; otherwise `false`.</returns>
    Task<bool> FileExistsAsync(string fileName, string path = "", CancellationToken token = default);

    /// <summary>
    /// Retrieves a file as a <see cref="Stream"/>.
    /// </summary>
    /// <param name="fileName">The name of the file to retrieve.</param>
    /// <param name="path">The location of the file to retrieve.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <exception cref="FileNotFoundException">The file does not exist.</exception>
    /// <returns>A Stream.</returns>
    Task<Stream> GetFileAsync(string fileName, string path = "", CancellationToken token = default);

    /// <summary>
    /// Retrieves a file as a <see cref="Stream"/>. If file retrieval succeeds, this method returns a
    /// <see cref="TryGetResponse"/> with the `Value` equal to the requested Stream and `Success` equal
    /// to `true. Otherwise, `Value` contains <see cref="Stream.Null"/> and `Success` equals `false`.
    /// </summary>
    /// <param name="fileName">The name of the file to retrieve.</param>
    /// <param name="path">The location of the file to retrieve.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    /// <returns>A TryGetResponse with `Success` = `true` if the file is found or `false` if the file
    /// is not found.</returns>
    Task<TryGetResponse> TryGetFileAsync(string fileName, string path = "", CancellationToken token = default);

    public sealed record TryGetResponse(bool Success, Stream Value) : IDisposable, IAsyncDisposable
    {
        public void Dispose() => Value.Dispose();
        public async ValueTask DisposeAsync() => await Value.DisposeAsync();
    }

    /// <summary>
    /// Deletes a file. If the file does not exist, the method returns without throwing an exception.
    /// </summary>
    /// <param name="fileName">The name of the file to delete.</param>
    /// <param name="path">The location of the file to delete.</param>
    /// <param name="token"><see cref="T:System.Threading.CancellationToken"/></param>
    Task DeleteFileAsync(string fileName, string path = "", CancellationToken token = default);
}
