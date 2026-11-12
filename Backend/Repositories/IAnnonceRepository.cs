using Core;

namespace Backend.Repositories;

public interface IAnnonceRepository
{
    List<Annonce> GetAll();

    Annonce? GetById(int Annonce_Id);

    List<Annonce> GetByOwnerId(string Owner_Id);

    Annonce CreateNewAd(Annonce annonce);

    bool DeleteAd(int Annonce_Id);

    bool UpdateAnnonce(Annonce annonce);

    bool UpdateAnnonceStatus(Annonce annonce);
   

}