using Core;

namespace Backend.Repositories;

public interface IAnnonceRepository
{
    List<Annonce> GetAll();

    Annonce? GetById(string annonce_id);

    List<Annonce> GetByOwnerId(string owner_id);

    Annonce CreateNewAd(Annonce annonce);

    bool DeleteAd(string annonce_id);

    bool UpdateAnnonce(Annonce annonce);

}