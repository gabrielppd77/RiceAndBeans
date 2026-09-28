// import { useEffect } from "react";

// import { Box, LinearProgress, Stack } from "@mui/material";

// import TextField from "@components/TextField";
// import ActionDialog from "@components/ActionDialog";
// import CurrencyTextField from "@components/CurrencyTextField";
// import AutoCompleteCategory from "@components/AutoCompleteCategory";
// import {UploadImage} from "@components/UploadImage";

// import { useProductCreate } from "@libs/queries/product/useProductCreate";
// import { useProductUpdate } from "@libs/queries/product/useProductUpdate";
// import { useProductGetById } from "@libs/queries/product/useProductGetById";
// import { useProductUpdateImage } from "@libs/queries/product/useProductUpdateImage";

// import { z } from "zod";

// import useValidateForm from "@hooks/useValidateForm";

// const schema = z.object({
//   name: z.string({ message: "Informe o Nome" }).min(1),
//   description: z.string().nullish(),
//   order: z.number({ message: "Informe a Ordem do Produto" }).min(1),
//   price: z.number({ message: "Informe o Preço" }),
//   categoryId: z.string({ message: "Informe a Categoria" }).min(1),
// });

// type DataType = z.infer<typeof schema>;

interface FormProps {
  id: string | null;
  onClose: () => void;
}

export default function Form({ id, onClose }: FormProps) {
  console.log({ id, onClose });
  // const { data, isLoading: _isLoading, isFetching } = useProductGetById({ id });

  // const isLoading = _isLoading || isFetching;

  // // const { FormProvider, handleSubmit, reset } = useValidateForm({
  // //   schema,
  // //   defaultValues: data,
  // // });

  // // useEffect(() => {
  // //   reset(data);
  // // }, [data, reset]);

  // const { mutateAsync: mutateAsyncCreate, isPending: isPendingCreate } =
  //   useProductCreate();
  // const { mutateAsync: mutateAsyncUpdate, isPending: isPendingUpdate } =
  //   useProductUpdate();
  // const {
  //   mutateAsync: mutateAsyncUpdateImage,
  //   isPending: isPendingUpdateImage,
  // } = useProductUpdateImage();

  // const isSubmitting = isPendingCreate || isPendingUpdate;

  // async function onSubmit(d: DataType) {
  //   if (id) {
  //     await mutateAsyncUpdate({
  //       params: { id },
  //       data: d,
  //     });
  //   } else {
  //     await mutateAsyncCreate({
  //       data: d,
  //     });
  //   }
  //   onClose();
  // }

  // async function handleUpdateImage(file: File) {
  //   if (!id) return;
  //   const formData = new FormData();
  //   formData.append("file", file);
  //   await mutateAsyncUpdateImage({
  //     data: formData,
  //     params: {
  //       id,
  //     },
  //   });
  // }

  return <></>;

  // return (
  //   <ActionDialog
  //     title="Cadastro de Produto"
  //     isLoading={isSubmitting}
  //     onClose={() => onClose()}
  //     onSubmit={handleSubmit(onSubmit)}
  //   >
  //     <Box height={4}>{isLoading && <LinearProgress />}</Box>
  //     <FormProvider>
  //       <Stack gap={1}>
  //         {id && (
  //           <Box
  //             sx={{
  //               display: "flex",
  //               alignItems: "center",
  //               justifyContent: "center",
  //             }}
  //           >
  //             <UploadImage
  //               alt="Imagem do produto"
  //               src={data?.urlImage}
  //               // onChange={(value) => handleUpdateImage(value[0])}
  //               isLoading={isLoading || isPendingUpdateImage}
  //             />
  //           </Box>
  //         )}

  //         <TextField required label="Nome" name="name" />
  //         <TextField label="Descrição" name="description" />
  //         <TextField
  //           required
  //           type="number"
  //           label="Ordem do Produto"
  //           name="order"
  //         />
  //         <CurrencyTextField required label="Preço" name="price" prefix="R$ " />
  //         <AutoCompleteCategory name="categoryId" />
  //       </Stack>
  //     </FormProvider>
  //   </ActionDialog>
  // );
}
