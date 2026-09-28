import { useRef } from "react";

import { Avatar, IconButton } from "@mui/material";
import { LinearProgress } from "../LinearProgress";

interface UploadImageProps {
  src?: string;
  alt?: string;
  isLoading?: boolean;
  onChange: (value: FileList) => void;
}

export function UploadImage({
  src,
  alt,
  isLoading,
  onChange,
}: UploadImageProps) {
  const inputRef = useRef<HTMLInputElement | null>(null);

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files;
    if (file) {
      onChange(file);
    }
  };

  return (
    <div>
      <div className="flex flex-col items-center justify-center gap-2">
        <IconButton>
          <Avatar
            alt={alt}
            src={src + "?time=" + new Date().getTime()}
            sx={{
              height: 180,
              width: 180,
            }}
            onClick={() => inputRef.current?.click()}
          />
        </IconButton>

        <LinearProgress isLoading={isLoading} />
      </div>

      <input
        ref={inputRef}
        type="file"
        className="sr-only"
        accept=".jpg, .jpeg, .png, .webp"
        onChange={handleFileChange}
      />
    </div>
  );
}
