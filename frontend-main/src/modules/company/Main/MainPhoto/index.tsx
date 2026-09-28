import { UploadImage } from "@modules/core/@components/UploadImage";
import { useUpdateGetFormData } from "@modules/company/@hooks/useGetFormData";
import { useUploadImage } from "@modules/company/@hooks/useUploadImage";

interface MainPhotoProps {
  urlImage?: string;
}

export function MainPhoto({ urlImage }: MainPhotoProps) {
  const { mutateAsync, isPending } = useUploadImage();
  const { handleChange } = useUpdateGetFormData();

  async function handleUploadImage(files: FileList) {
    const formData = new FormData();
    formData.append("file", files[0]);
    const imageUrl = await mutateAsync({
      data: formData,
    });
    handleChange({ urlImage: imageUrl });
  }

  return (
    <div className="flex flex-col gap-2">
      <p className="text-lg font-medium">Foto principal</p>
      <UploadImage
        onChange={handleUploadImage}
        src={urlImage}
        alt="Foto principal"
        isLoading={isPending}
      />
    </div>
  );
}
