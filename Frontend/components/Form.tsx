interface FormProps
  extends React.PropsWithChildren<React.HTMLAttributes<HTMLFormElement>> {}

const FormRoot = ({ children, ...props }: FormProps) => {
  return <form {...props}>{children}</form>;
};

interface FormHeaderProps extends React.HTMLAttributes<HTMLDivElement> {
  title: string;
  description: string;
}

const FormHeader = ({ title, description, ...props }: FormHeaderProps) => {
  return (
    <div {...props}>
      <h2 className="text-base font-semibold leading-7 text-white">{title}</h2>
      <p className="mt-1 text-sm leading-6 text-gray-400">{description}</p>
    </div>
  );
};

interface FormSectionProps
  extends React.PropsWithChildren<React.HTMLAttributes<HTMLDivElement>> {}

const FormSection = ({ children, ...props }: FormSectionProps) => {
  return (
    <div className="pb-6" {...props}>
      <div className="mt-10 grid grid-cols-1 gap-x-6 gap-y-8 sm:grid-cols-6">
        {children}
      </div>
    </div>
  );
};

export let Form = Object.assign(FormRoot, {
  Header: FormHeader,
  Section: FormSection,
});
