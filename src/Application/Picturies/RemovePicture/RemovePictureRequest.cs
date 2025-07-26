namespace Application.Picturies.RemovePicture;

public record RemovePictureRequest(
    string EntityType,
    Guid EntityId);