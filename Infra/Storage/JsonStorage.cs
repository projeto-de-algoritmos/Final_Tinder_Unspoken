using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Domain.Models;

namespace Infra.Storage
{
    public class JsonStorage<T> where T : User
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly List<T> _context;
        private readonly JsonStorageOptions _options;
        public IEnumerable<T> Context => _context;
        public JsonStorage()
        {
            _options = new JsonStorageOptions();
            _filePath = _options.FilePath;
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            _context = ReadFile().ToList();
        }

        public async Task<T> CreateAsync(T model)
        {
            try
            {
                var id = _context.Any() ? _context.LastOrDefault().Id + 1 : _context.Count() + 1;
                model.Id = id;
                model.CreatedAt = DateTime.Now;

                _context.Add(model);

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return _context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return _context.FirstOrDefault(x => x.Id == id);
        }

        public async Task<T> RemoveAsync(int id)
        {
            try
            {
                var model = await GetByIdAsync(id);
                if (model is null)
                    throw new Exception($"${typeof(T)} não encontrado");

                if (!_context.Remove(model))
                    throw new Exception($"Falha ao remover ${typeof(T)}");

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task SaveAsync()
        {
            try
            {
                if (!FileExists())
                    File.Create(_filePath).Close();

                await File.WriteAllTextAsync(
                    _filePath,
                    JsonSerializer.Serialize(_context, _jsonSerializerOptions),
                    Encoding.UTF8
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IEnumerable<T> ReadFile()
        {
            if (!FileExists())
                return new List<T>();

            var result = File.ReadAllText(_filePath);

            if (string.IsNullOrEmpty(result))
                return new List<T>();

            try
            {
                return JsonSerializer.Deserialize<IEnumerable<T>>(result);
            }
            catch (JsonException ex)
            {
                throw new FileLoadException($"Erro ao iniciar arquivo: ${ex}");
            }
        }

        private bool FileExists()
        {
            return File.Exists(_filePath);
        }
    }

}
