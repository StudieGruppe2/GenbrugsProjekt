using Core;

namespace Backend.Repositories;

public interface IAnnonceRepository
{
    List<Annonce> GetAll();

    Annonce? GetById(string annonceId);

    List<Annonce> GetByOwnerId(string ownerId);

    Annonce CreateNewAd(Annonce annonce);

    bool DeleteAdd(string annonceId);

    bool UpdateAnnonce(Annonce annonce);
}
