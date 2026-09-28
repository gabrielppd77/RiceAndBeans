import { UploadImage } from "@modules/core/@components/UploadImage";
import { useUpdateGet } from "@modules/product/@hooks/useGet";
import { useUploadImage } from "@modules/product/@hooks/useUploadImage";

interface MainPhotoProps {
  productId: string;
  urlImage?: string | null;
}

export function MainPhoto({ productId, urlImage }: MainPhotoProps) {
  const { mutateAsync, isPending } = useUploadImage();
  const { handleChange } = useUpdateGet();

  async function handleUploadImage(files: FileList) {
    const formData = new FormData();
    formData.append("file", files[0]);
    const imageUrl = await mutateAsync({
      data: formData,
      params: {
        productId,
      },
    });
    handleChange(productId, { urlImage: imageUrl });
  }

  return (
    <div className="flex flex-col gap-2">
      <p className="text-lg font-medium">Foto principal</p>
      <UploadImage
        onChange={handleUploadImage}
        src={urlImage || undefined}
        alt="Foto principal"
        isLoading={isPending}
      />
    </div>
  );
}
